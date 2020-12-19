using PSCommenceModules.Base;
using System.Management.Automation;

namespace PSCommenceModules.CmdLets
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