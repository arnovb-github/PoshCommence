using PSCommenceModules.Base;
using System.Management.Automation;

namespace PSCommenceModules.CmdLets
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