using PSCommenceModules.Base;
using System.Management.Automation;
using Vovin.CmcLibNet.Export;

namespace PSCommenceModules.CmdLets
{

    [Cmdlet(VerbsCommon.Get, "CmcExportFormats")]
    public class GetCmcExportFormats : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            foreach (var o in EnumHelper.ListEnum<ExportFormat>())
            {
                WriteObject(o);
            }
        }
    }
}