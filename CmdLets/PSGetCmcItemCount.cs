using PoshCommence.Base;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcItemCount")]
    public class GetCmcItemCount : PSCmdlet
    {
        private string categoryName;
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [Alias("c")]
        public string CategoryName
        {              
            get { return categoryName; }
            set { categoryName = value; }
        }

        private ICommenceDatabase db;

        protected override void BeginProcessing()
        {
            db = new CommenceDatabase();
        }
        protected override void ProcessRecord()
        {
            var retval = new 
            {
                CategoryName = this.CategoryName,
                ItemCount = db.GetItemCount(CategoryName)
            };
            WriteObject(retval);
        }
        
        protected override void EndProcessing()
        {
            db.Close();
        }

        protected override void StopProcessing()
        {
            db.Close();
        }
    }
}