using PoshCommence.Base;
using System.Management.Automation;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcIniFile")]
    public class GetCmcIniFile  : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(PathFinder.IniFile);
        }
    }
}