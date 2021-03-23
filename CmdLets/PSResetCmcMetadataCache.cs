using PoshCommence.Base;
using System.Management.Automation;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Reset, "CmcMetadataCache")]
    public class ResetMetadataCache : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            CommenceMetadata.ClearAll();
            WriteVerbose("Cleared the Commence Metadata cache");
        }
    }
}
