using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PSCommenceModules.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcLastDDEError")]
    public class GetCmcLastDDEError  : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            using (var db = new CommenceDatabase())
            {
                WriteObject(db.GetLastError());
            }
        }
    }
}