using System.Management.Automation;

namespace PSCommenceModules
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