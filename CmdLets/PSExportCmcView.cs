using PSCommenceModules.Base;
using System.Management.Automation;
using System.Collections.Generic;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Export;


namespace PSCommenceModules.CmdLets
{
    /*  This CmdLet comprises a limited set of export features as exposed by Vovin.CmcLibNet.
        For the complete experience refer to the Vovin.CmcLibNet documentation.
        The most notable difference is that you cannot define related fields.
        Also, some advanced options are not available because the list of parameters
        would become very long.
        This is exporting done quick and dirty by design.
    */
    [Cmdlet(VerbsData.Export, "CmcView")]
    public class ExportCmcView : PSCmdlet
    {
        private IExportSettings _exportOptions = new ExportSettings();
        private string _viewName;
        [Parameter(Position = 0, Mandatory = true)]
        public string ViewName
        {
        get { return _viewName; }
        set { _viewName = value; }
        }

        private string _path;
        [Parameter(Position = 1, Mandatory = true)]
        public string Path
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
        
        protected override void ProcessRecord()
        {
            CommenceExporter.ExportView(_viewName, _path, _exportOptions);
            WriteVerbose("Export done. Hopefully");
        }
    }
}