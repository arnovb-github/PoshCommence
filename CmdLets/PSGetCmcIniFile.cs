using PoshCommence.Base;
using System.Management.Automation;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcIniFile")]
    public class GetCmcIniFile  : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            var item = InvokeProvider.Item.Get(PathFinder.IniFile.FullName);
            WriteObject(item, true);
        }
    }
}