using System.Management.Automation;
using System.Collections.Generic;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;
using PoshCommence.Base;

namespace PoshCommence.CmdLets
{
    // This CmdLet is only for debugging!
    [Cmdlet(VerbsDiagnostic.Test, "Completer")]
    public class TestCompleter  : PSCmdlet
    {
        private string categoryName;
        [Parameter(Position = 0, Mandatory = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [Alias("c")]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        
        private string fieldName;
        [Parameter()]
        [ArgumentCompleter(typeof(FieldNameArgumentCompleter))]
        [Alias("f")]
        public string Field
        {
            get { return fieldName; }
            set { fieldName = value; }
        }
        

        protected override void ProcessRecord()
        {
            WriteObject("Hello, World!");
        }

    }
}