using System.Management.Automation;

namespace PSCommenceModules
{
    [Cmdlet(VerbsCommon.Get, "CmcDatabaseName")]
    public class GetCmcDatabaseName  : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(PathFinder.DatabaseName);
        }
    }
}