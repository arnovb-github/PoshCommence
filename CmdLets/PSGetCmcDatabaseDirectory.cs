using PSCommenceModules.Base;
using System.Management.Automation;

namespace PSCommenceModules.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcDatabaseDirectory")]
    public class GetCmcDatabaseDirectory  : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(PathFinder.DatabaseDirectory);
        }
    }
}