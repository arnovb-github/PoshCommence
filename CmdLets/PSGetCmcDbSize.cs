using System.IO;
using System.Linq;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;


namespace PSCommenceModules.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcDbSize")]
    public class GetCmcDbSize  : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(GetDbSize());
        }

        private long GetDbSize() 
        {
            using (ICommenceDatabase db = new CommenceDatabase())
            {
                //return db.GetDatabaseSchema().Size; // this is too complicated. It does a ton of unneeded DDE
                return Directory.GetFiles(db.Path, "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length));
            }
        }
    }
}