using PoshCommence.Base;
using System.Management.Automation;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Clear, "CmcMetadataCache")]
    public class ClearMetadataCache : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            CommenceMetadata.ClearAll();
            WriteVerbose("Cleared the Commence Metadata cache.");
        }
    }
}
