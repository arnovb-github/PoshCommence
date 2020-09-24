using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PSCommenceModules
{
    public class PSCommenceModules
    {
        [Cmdlet(VerbsCommon.Get, "CmcFieldValues")]
        public class GetCmcFields : PSCmdlet
        {
            private string categoryName;
            [Parameter(Position = 0, Mandatory = true)]
            public string CategoryOrViewName
            {
            get { return categoryName; }
            set { categoryName = value; }
            }
            private string[] fieldNames;
            [Parameter(Position = 1, Mandatory = true)]
            public string[] FieldNames
            {
                get { return fieldNames; }
                set { fieldNames = value; }
            }

            // use this to pass in a viewname
            private bool useView;
            [Parameter()]
            public SwitchParameter UseView
            {
                get { return useView; }
                set { useView = value; }
            }
            
            // thids switch parameter
            private bool useThids;
            [Parameter()]
            public SwitchParameter UseThids
            {
                get { return useThids; }
                set { useThids = value; }
            }
            
            private ICursorFilter[] filters;
            [Parameter()]
            public ICursorFilter[] Filters
            {
                get { return filters; }
                set { filters = value; }
            }
            
            private RelatedColumn[] relatedColumns;
            [Parameter()]
            public RelatedColumn[] RelatedColumns
            {
                get { return relatedColumns; }
                set { relatedColumns = value; }
            }
            protected override void ProcessRecord()
            {

                // assemble all the columnnames
                var columnNames = new List<string>();
                if (useThids) 
                {
                    columnNames.Add("thid");
                }
                foreach (var s in fieldNames)
                {
                    columnNames.Add(s);
                }
                if (relatedColumns != null)
                {
                    var r = relatedColumns.Select(s => s.ColumnName);
                    columnNames = columnNames.Concat(r).ToList();
                }

                // do the data reading
                var rows = Helper.GetCmcFieldValues(categoryName,
                    fieldNames,
                    filters,
                    relatedColumns,
                    useView,
                    useThids);

                foreach (var row in rows)
                {
                    List<CommenceField> l = new List<CommenceField>();
                    // enrich the output by assembling the fieldvalues into proper objects
                    // because you never know when it comes in handy
                    // Explicit declaration of CommenceField isn't strictly needed,
                    // We could also have used just an anonymous type
                    for (int i = 0; i < row.Count; i++)
                    {
                        l.Add(new CommenceField(
                            categoryName, // a little pointless, but it may come in handy when comparing results from several calls
                            columnNames[i],
                            row[i])
                            );
                    }
                    WriteObject(l, false); // return and do not enumerate. I.e. pass every row separately.
                    WriteVerbose($"Commence row contains values returned as {l.GetType()}. Use foreach to iterate over them.");
                }
            }
        }

        public class CommenceField
        {
            internal CommenceField(string categoryName, 
                            string fieldName, 
                            string fieldValue)
            {
                CategoryName = categoryName;
                FieldName = fieldName;
                FieldValue = fieldValue;
            }
            public string CategoryName { get;} 
            public string FieldName { get; }
            public string FieldValue { get; }
        }

        private static class Helper
        {
            // notice we do not do any error checking
            // we just let the error pass through to Powershell
            // Not sure if that's a good idea
            public static List<List<string>> GetCmcFieldValues(
                string categoryName,
                 IEnumerable<string> fieldNames,
                 IEnumerable<ICursorFilter> filters,
                 IEnumerable<RelatedColumn> relatedColumns,
                 bool useView,
                 bool useThids)
            {
                var retval = new List<List<string>>();
                using (var db = new CommenceDatabase())
                {
                    using (var cur = db.GetCursor(categoryName,
                        useView ? CmcCursorType.View : CmcCursorType.Category,
                        useThids ? CmcOptionFlags.UseThids : CmcOptionFlags.Default))
                    {
                        if (filters != null)
                        {
                            foreach (var f in filters)
                            {
                                cur.Filters.Add(f);
                            }
                        
                        cur.Filters.Apply();
                        }
                        cur.SetColumns(fieldNames.Cast<object>().ToArray());
                        if (relatedColumns != null) 
                        {
                            foreach (var rc in relatedColumns)
                            {
                                cur.SetRelatedColumn(cur.ColumnCount, 
                                    rc.ConnectionName,
                                    rc.ToCategory,
                                    rc.FieldName);
                            }
                        }
                        retval = cur.ReadAllRows(); // perhaps we should do our transformation to CommenceField here
                    }
                }
                return retval;
            }
        } 
    }

    public class RelatedColumn
    {
        string delim = "%%";
        public RelatedColumn() {}
        public RelatedColumn(string c, string t, string f)
        {
            ConnectionName = c;
            ToCategory = t;
            FieldName = f;
        }
        public string ConnectionName { get; set; }
        public string ToCategory { get; set; }
        public string FieldName { get; set; }

        // create a columnname
        internal string ColumnName => '%'+ ConnectionName + delim  + ToCategory + delim + FieldName + '%';

    }
}
