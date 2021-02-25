using PoshCommence.Base;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
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

            PSObject responseObject = new PSObject();
            foreach (var row in rows)
            {
                for (int i = 0; i < row.Count; i++) {
                    responseObject.Members.Add(new PSNoteProperty(columnNames[i], row[i]));
                }
                WriteObject(responseObject);
            }
        }
    }
}
