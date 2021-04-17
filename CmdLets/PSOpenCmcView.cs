using PoshCommence.Base;
using System;
using System.Diagnostics;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Open, "CmcView")]
    public class OpenCmcView : PSCmdlet
    {
        private const string COMMENCE_PROCESS = "commence";

        private string viewName;
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("v","ViewName")]
        public string Name
        {
            get { return viewName; }
            set { viewName = value; }
        }


        private bool newCopy;
        [Parameter(Position = 1, Mandatory = false)]
        public SwitchParameter NewCopy
        {
            get { return newCopy; }
            set { newCopy = value; }
        }

        [Parameter(Position = 2, Mandatory = false)]
        [ValidateRange(1,99)]
        [PSDefaultValue(Value = 5)] // PlatyPS does not pick this up
        public int Max
        {
            get { return maxViews; }
            set { maxViews = value; }
        }

        private int maxViews = 5;
        private int counter;
        private string dbName;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            counter = 0; // not needed, just there to make the logic flow more clear
        }

        protected override void ProcessRecord()
        {
            counter++;
            if (counter >= maxViews) { return; }
            using (var db = new CommenceDatabase()) // we could move this to BeginProcessing? Do we care? 
            {
                dbName = db.Name;
                if (!db.ShowView(this.Name, newCopy))
                {
                    throw new InvalidOperationException($"Unable to open view '{this.Name}' in Commence.");
                }
            }
        }

        protected override void EndProcessing()
        {
            base.StopProcessing();
            ShowCommence(dbName);
            if (counter >= maxViews)
            {
                WriteWarning($"Number of views exceeded limit of this cmdlet, opened first {this.Max} views.");
            }
        }

        private void ShowCommence(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName)) { return; }
            Process p = ProcessHelper.GetProcessFromTitle(COMMENCE_PROCESS, databaseName);
            if (p != null)
            {
                WindowHelper.BringProcessToFront(p);
            }
        }

    }
}