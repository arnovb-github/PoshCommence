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
            using (ICommenceDatabase db = new CommenceDatabase())
            {
                //return db.GetDatabaseSchema().Size; // this is too complicated. It does a ton of unneeded DDE
                long size = Directory.GetFiles(db.Path, "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length));
                WriteObject(size);
                WriteVerbose($"The total size of all files in database path {db.Path} is {size} bytes");
            }
        }

    }
}