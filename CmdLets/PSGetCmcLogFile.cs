using PoshCommence.Base;
using System.Management.Automation;

namespace PoshCommence.CmdLets
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