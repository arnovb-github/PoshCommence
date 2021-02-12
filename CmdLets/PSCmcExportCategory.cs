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
    [Cmdlet(VerbsData.Export, "CmcCategory")]
    public class CmcExportCategory : PSCmdlet
    {
        private IExportSettings _exportOptions = new ExportSettings();
        private string _categoryName;
        [Parameter(Position = 0, Mandatory = true)]
        public string CategoryName
        {
        get { return _categoryName; }
        set { _categoryName = value; }
        }

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
        [Parameter()]
        public ICursorFilter[] Filters
        {
            get { return _filters; }
            set { _filters = value; }
        }
        
        // we do not use an IEnumerable<string>,
        // because we want to be able to pass a PS @() array in
        private string[] _fieldNames;
        [Parameter()]
        public string[] FieldNames
        {
            get { return _fieldNames; }
            set { _fieldNames = value; }
        }

        [Parameter()]
        public SwitchParameter SkipConnectedItems
        {
            get { return _exportOptions.SkipConnectedItems; }
            set { _exportOptions.SkipConnectedItems = value; }
        }

        [Parameter()]
        public SwitchParameter UseThids
        {
            get { return _exportOptions.UseThids; }
            set { _exportOptions.UseThids = value; }
        }
        
        protected override void ProcessRecord()
        {
            if (_fieldNames != null) 
            {
                CommenceExporter.ExportCursor(_categoryName, _filters, _fieldNames, _path, _exportOptions);
                return;
            }
            CommenceExporter.ExportCategory(_categoryName, _filters, _path, _exportOptions);
            WriteVerbose("Export done. Hopefully");
        }

    }
}