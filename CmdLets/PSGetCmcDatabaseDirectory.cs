using PoshCommence.Base;
using System.Management.Automation;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcDatabaseDirectory")]
    public class GetCmcDatabaseDirectory  : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            var item = InvokeProvider.Item.Get(PathFinder.DatabaseDirectory.FullName);
            WriteObject(item, true);
        }
    }
}