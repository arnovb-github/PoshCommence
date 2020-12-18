using System.Management.Automation;

namespace PSCommenceModules
{
    [Cmdlet(VerbsCommon.Get, "CmcLogFile")]
    public class GetCmcLogFile  : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(PathFinder.LogFile);
        }
    }
}