using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcPreference")]
    public class GetCmcPreference : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        [ValidateSet("Me", "MeCategory", "LetterLogDir", "ExternalDir")]
        public string Preference { get; set; }
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            using (ICommenceDatabase db = new CommenceDatabase())
            {
                WriteObject(db.GetPreference(Preference));
            }
        }
    }
}
