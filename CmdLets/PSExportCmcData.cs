using PoshCommence.Base;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Export;


namespace PoshCommence.CmdLets
{
    /*  This CmdLet comprises a limited set of export features as exposed by Vovin.CmcLibNet.
        For the complete experience refer to the Vovin.CmcLibNet documentation.
        The most notable difference is that you cannot define related fields.
        Also, some advanced options are not available because the list of parameters
        would become very long.
        This is exporting done quick and dirty by design.
    */
    [Cmdlet(VerbsData.Export, "CmcData", DefaultParameterSetName = categorySet)]
    public class ExportCmcData : PSCmdlet
    {
        private const string categorySet = "ByCategory";
        private const string viewSet = "ByView";

        [Parameter(Position = 0,Mandatory = true, ParameterSetName = viewSet)]
        [ArgumentCompleter(typeof(ViewNameArgumentCompleter))]
        [Alias("v")]
        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }
        private string viewName;

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = categorySet)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [Alias("c")]
        public string CategoryName
        {
        get { return categoryName; }
        set { categoryName = value; }
        }
        private string categoryName;

        private IExportSettings _exportOptions = new ExportSettings();
        private string _path;
        [Parameter(Position = 1, Mandatory = true)]
        public string OutputPath
        {
        get { return _path; }
        set { _path = value; }
        }

        [Parameter()]
        public ExportFormat ExportFormat
        {
            get { return _exportOptions.ExportFormat; }
            set { _exportOptions.ExportFormat = value; }
        }
        
        // we do not use an IEnumerable<ICursorFilter>,
        // because we want to be able to pass a PS @() array in
        private ICursorFilter[] _filters;
        [Parameter(ParameterSetName = categorySet)]
        public ICursorFilter[] Filters
        {
            get { return _filters; }
            set { _filters = value; }
        }
        
        // we do not use an IEnumerable<string>,
        // because we want to be able to pass a PS @() array in
        private string[] fieldNames;
        [Parameter(ParameterSetName = categorySet)]
        [ArgumentCompleter(typeof(FieldNameArgumentCompleter))]
        public string[] FieldNames
        {
            get { return fieldNames; }
            set { fieldNames = value; }
        }

        private ConnectedField[] connectedFields;
        [Parameter(ParameterSetName = categorySet)]
        public ConnectedField[] ConnectedFields
        {
            get { return connectedFields; }
            set { connectedFields = value; }
        }

        [Parameter()]
        public SwitchParameter SkipConnectedItems
        {
            get { return _exportOptions.SkipConnectedItems; }
            set { _exportOptions.SkipConnectedItems = value; }
        }

        [Parameter(ParameterSetName = categorySet)]
        public SwitchParameter UseThids
        {
            get { return _exportOptions.UseThids; }
            set { _exportOptions.UseThids = value; }
        }

        [Parameter()]
        public SwitchParameter PreserveAllConnections
        {
            get { return _exportOptions.PreserveAllConnections; }
            set { _exportOptions.PreserveAllConnections = value; }
        }

        private bool useColumnNames;
        [Parameter(ParameterSetName = viewSet)]
        public SwitchParameter UseColumnNames
        {
            get { return useColumnNames; }
            set { useColumnNames = value; }
        }
        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(CategoryName)) // export view
            {
                ExportByView();
            }
            else
            {
                ExportByCategory();
            }
            WriteVerbose($"File '{_path}' of type '{_exportOptions.ExportFormat}' written to '{_path}'.");
            var item = InvokeProvider.Item.Get(_path);
            WriteObject(item, true);
        }

        private void ExportByView()
        {
            if (UseColumnNames)
            {
                _exportOptions.HeaderMode = HeaderMode.Columnlabel;
            }
            CommenceExporter.ExportView(viewName, _path, _exportOptions);
        }

        private void ExportByCategory()
        {
            if (fieldNames!= null || connectedFields != null)
            {
                CommenceExporter.ExportCursor(categoryName, _filters, fieldNames, connectedFields, _path, _exportOptions);
            }
            else
            {
                CommenceExporter.ExportCategory(categoryName, _filters, _path, _exportOptions);
            }
        }
    }
}