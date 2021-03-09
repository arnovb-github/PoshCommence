using PoshCommence.Base;
using System.Diagnostics;
using System;
using System.Linq;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Open, "CmcView")]
    public class OpenCmcView : PSCmdlet
    {
        private const string COMMENCE_PROCESS = "commence";
        private const int MAX_ALLOWED_VIEWS = 10;

        private IViewDef[] views;
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline=true)]
        public IViewDef[] View
        {
            get { return views; }
            set { views = value; }
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
            if (views.Length > MAX_ALLOWED_VIEWS) {
                WriteWarning($"Number of views exceeds limit of this cmdlet, opening first {MAX_ALLOWED_VIEWS} views.");
            }
            using (var db = new CommenceDatabase())
            {
                foreach (var v in views.Take(MAX_ALLOWED_VIEWS)) 
                {
                    if (!db.ShowView(v.Name, newCopy)) 
                    {
                        throw new InvalidOperationException($"Unable to open view '{v.Name}' in Commence.");
                    }
                }
                ShowCommence(db.Name);                
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