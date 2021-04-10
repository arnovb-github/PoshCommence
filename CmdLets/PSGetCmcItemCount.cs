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

        // TODO: would it be useful to also include a way to return the item count for a view?

        protected override void ProcessRecord()
        {
            using (var db = new CommenceDatabase())
            {
                //var retval = new 
                //{
                //    CategoryName = this.CategoryName,
                //    RowCount = db.GetItemCount(CategoryName)
                //};
                WriteObject(db.GetItemCount(CategoryName));
            }
        }
    }
}