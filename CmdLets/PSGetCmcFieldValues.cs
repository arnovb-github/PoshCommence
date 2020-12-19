using PSCommenceModules.Base;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PSCommenceModules.CmdLets
{

    [Cmdlet(VerbsCommon.Get, "CmcFieldValues")]
    public class GetCmcFieldValues : PSCmdlet
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
            var rows = CursorReader.GetCmcFieldValues(categoryName,
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
}
