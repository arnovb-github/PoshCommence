using PoshCommence.Base;
using System.Management.Automation;
using Vovin.CmcLibNet.Export;

namespace PoshCommence.CmdLets
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