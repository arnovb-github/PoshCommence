using PoshCommence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcData",
        DefaultParameterSetName = BY_CAT)]
    public class GetCmcData : PSCmdlet
    {
        private const string BY_CAT = "ByCategory";
        private const string BY_VIEW = "ByView";
        private readonly string connDelim = "%%";

        private string categoryName;
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = BY_CAT)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]        
        [Alias("c")]
        public string CategoryName
        {
        get { return categoryName; }
        set { categoryName = value; }
        }

        private string viewName;
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = BY_VIEW)]
        [ArgumentCompleter(typeof(ViewNameArgumentCompleter))]
        [Alias("v")]
        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }

        private string[] fieldNames;
        [Parameter(Position = 1, Mandatory = true, ParameterSetName = BY_CAT)]
        [Parameter(Position = 1, Mandatory = false, ParameterSetName = BY_VIEW)]
        [ArgumentCompleter(typeof(FieldNameArgumentCompleter))]
        public string[] FieldNames
        {
            get { return fieldNames; }
            set { fieldNames = value; }
        }


        [Parameter(ParameterSetName = BY_CAT)]
        public SwitchParameter UseThids { get; set; }
        
        private ICursorFilter[] filters;
        [Parameter()]
        public ICursorFilter[] Filters
        {
            get { return filters; }
            set { filters = value; }
        }
        
        private ConnectedField[] connectedFields;
        [Parameter()]
        public ConnectedField[] ConnectedFields
        {
            get { return connectedFields; }
            set { connectedFields = value; }
        }

        private List<string> columnLabels;
        [Parameter(ParameterSetName = BY_VIEW)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter ColumnLabels { get; set;}

        private List<string> columnNames;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (!string.IsNullOrEmpty(ViewName) && FieldNames == null && ConnectedFields == null)
            {
                SetPropertiesByView(ViewName);
            }
            columnNames = GetColumnNames();
        }

        protected override void ProcessRecord()
        {
            // do the data reading
            // this can take a very long time
            // TODO: we need a way to warn the user
            var rows = CursorReader.GetCmcFieldValues(this.CursorName,
                FieldNames,
                Filters,
                ConnectedFields,
                !string.IsNullOrEmpty(ViewName),
                UseThids);

            // if columnlabels contain a duplicate,
            // you get really strange results
            // before continuing, we should check for that and warn the user
            if (!(columnLabels is null) && columnLabels.Distinct().Count() < columnLabels.Count )
            {
                WriteWarning("Duplicate columnlabels were detected, output will use columnnames (i.e. fieldnames) instead.");
                ColumnLabels = false;
            }

            foreach (var row in rows)
            {
                PSObject responseObject = new PSObject();
                for (int i = 0; i < row.Count; i++) { //row.Count represents the number of columns in the row, except when it only contains indirect fields and the viewtype is Book. What a mess Commence is.
                    // PSNoteProperty: Serves as a property that is a simple name-value pair.
                    if (ColumnLabels)
                    {
                        responseObject.Members.Add(new PSNoteProperty(columnLabels[i], row[i]));
                    }
                    else 
                    {
                        responseObject.Members.Add(new PSNoteProperty(columnNames[i], row[i]));
                    }
                }
                WriteObject(responseObject);
            }
        }

        private string CursorName =>
            string.IsNullOrEmpty(ViewName) ?  this.CategoryName : this.ViewName;

        private List<string> GetColumnNames()
        {
            // assemble all the columnnames used for output object
            var retval = new List<string>();
            if (UseThids)
            {
                retval.Add("thid");
            }

            var f = FieldNames?.Select(s => s);
            if (f != null) { retval.AddRange(f); }

            if (ConnectedFields != null)
            {
                var r = connectedFields.Select(s => s.ColumnName);
                retval = retval.Concat(r).ToList();
            }
            return retval;
        }

        private void SetPropertiesByView(string viewName) // this is very flawed
        {
            // if we have just a view, everything changes
            // we need to create the fieldnames and the connected fields from the columns in the view.
            // This is non-trivial, because of the mess Commence makes with representing rlated columns.
            // for some viewtypes it is '%%', for others it is a space.
            IEnumerable<ICommenceConnection> connNames;
            IEnumerable<string> viewColumns;
            List<string> directColumns = new List<string>();
            List<ConnectedField> connectedColumns = new List<ConnectedField>();

            using (var db = new CommenceDatabase())
            using (var cur = db.GetCursor(viewName, CmcCursorType.View, CmcOptionFlags.Default))
            {
                connNames = db.GetConnectionNames(cur.Category);
                viewColumns = db.GetViewColumnNames(viewName); // get fields in view by fieldname
                columnLabels = db.GetViewColumnNames(viewName, CmcOptionFlags.Default).ToList();

                // loop through all columns to determine if they are a direct field or a connection
                foreach (var vc in viewColumns)
                {
                    if (vc.Contains(connDelim))
                    {
                        string[] cc = vc.Split(new string[] { connDelim }, StringSplitOptions.None);
                        if (cc.Length == 3)
                        {
                            connectedColumns.Add(new ConnectedField(cc[0], cc[1], cc[2]));
                        }
                    }
                    else if (vc.Contains(' '))
                    {
                        foreach (ICommenceConnection c in connNames)
                        {
                            if (vc.StartsWith(c.Name) && vc.EndsWith(c.ToCategory)
                                && vc.Length == c.Name.Length + c.ToCategory.Length + 1)
                            {
                                connectedColumns.Add(new ConnectedField(c.Name, c.ToCategory, db.GetNameField(c.ToCategory)));
                                break;
                            }
                        }
                        directColumns.Add(vc); // no matching connection, field with space must be a direct field
                    }
                    else // a direct column
                    {
                        directColumns.Add(vc);
                    }
                }

                FieldNames = directColumns?.ToArray();
                ConnectedFields = connectedColumns?.ToArray();
            }
        }
    }
}
