using PoshCommence.Base;
using System.Diagnostics;
using System.Management.Automation;
using Vovin.CmcLibNet;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Open, "CmcView")]
    public class OpenCmcView : PSCmdlet
    {
        private const string COMMENCE_PROCESS = "commence";

        private IViewDef view;
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline=true)]
        public IViewDef Name
        {
            get { return view; }
            set { view = value; }
        }

        private bool newCopy;
        [Parameter(Position = 1)]
        public SwitchParameter NewCopy
        {
            get { return newCopy; }
            set { newCopy = value; }
        }
        
        protected override void ProcessRecord()
        {
            using (var db = new CommenceDatabase())
            {
                if (db.ShowView(view.Name, newCopy)) 
                {
                    ShowCommence(db.Name);
                }
                else
                {
                    throw new CommenceDDEException($"Commence could not open a view named '{view.Name}'.");
                }
            }
        }

        private void ShowCommence(string databaseName)
        {
            Process p = ProcessHelper.GetProcessFromTitle(COMMENCE_PROCESS, databaseName);
            if (p != null) {
                WindowHelper.BringProcessToFront(p);
            }
        }
        
    }
}