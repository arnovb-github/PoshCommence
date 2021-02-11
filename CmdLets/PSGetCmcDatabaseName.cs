using PoshCommence.Base;
using System.Management.Automation;

namespace PoshCommence.CmdLets
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