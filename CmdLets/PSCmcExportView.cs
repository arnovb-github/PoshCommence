using PoshCommence.Base;
using System.Management.Automation;
using Vovin.CmcLibNet.Export;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsData.Export, "CmcView")]
    public class CmcExportView : PSCmdlet
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
        
        protected override void ProcessRecord()
        {
            CommenceExporter.ExportView(_viewName, _path, _exportOptions);
            WriteVerbose("Export done. Hopefully");
        }
    }
}