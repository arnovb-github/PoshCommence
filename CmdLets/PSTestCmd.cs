using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsDiagnostic.Test, "Cmd")] // ConfirmImpact = None will supress a superfluous prompt that may occur depending on user preference settings
    public class TestCmd : PSCmdlet
    {

        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        private string categoryName;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            var cat = CategoryName; // sets it just once
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteObject(this.CategoryName);
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}
