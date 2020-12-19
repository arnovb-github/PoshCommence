using PSCommenceModules.Base;
using System.Management.Automation;

namespace PSCommenceModules.CmdLets
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