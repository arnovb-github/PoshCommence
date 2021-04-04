using PoshCommence.Base;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcData",
        DefaultParameterSetName = "ByCategory")]
    public class GetCmcData : PSCmdlet
    {
        private string categoryName;
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "ByCategory")]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]        
        [Alias("c")]
        public string CategoryName
        {
        get { return categoryName; }
        set { categoryName = value; }
        }

        private string viewName;
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "ByView")]
        [ArgumentCompleter(typeof(ViewNameArgumentCompleter))]
        [Alias("v")]

        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }

        private string[] fieldNames;
        [Parameter(Position = 1, Mandatory = true)]
        [ArgumentCompleter(typeof(FieldNameArgumentCompleter))]
        public string[] FieldNames
        {
            get { return fieldNames; }
            set { fieldNames = value; }
        }

        private bool useThids;
        [Parameter(ParameterSetName = "ByCategory")]
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
            var rows = CursorReader.GetCmcFieldValues(GetCursorName(),
                fieldNames,
                filters,
                relatedColumns,
                !string.IsNullOrEmpty(viewName),
                useThids);

            foreach (var row in rows)
            {
                PSObject responseObject = new PSObject();
                for (int i = 0; i < row.Count; i++) {
                    // PSNoteProperty: Serves as a property that is a simple name-value pair.
                    responseObject.Members.Add(new PSNoteProperty(columnNames[i], row[i]));
                }
                WriteObject(responseObject);
            }
        }

        private string GetCursorName()
        {
            return string.IsNullOrEmpty(viewName) ?  categoryName : viewName;
        }
    }
}
