using System.Management.Automation;

namespace PSCommenceModules
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