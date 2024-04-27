using PoshCommence.Base;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Find, "CmcDuplicates")]
    public class FindDuplicates : PSCmdlet
    {
        private string categoryName;
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("c")]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private string fieldName;
        [Parameter(Position = 1, Mandatory = true)]
        [ArgumentCompleter(typeof(FieldNameArgumentCompleter))]
        [Alias("f")]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }
        protected override void ProcessRecord()
        {
            using (ICommenceDatabase db = new CommenceDatabase())
            using (ICommenceCursor cur = db.GetCursor(categoryName, CmcCursorType.Category, CmcOptionFlags.Canonical))
            {
                WriteObject(cur.HasDuplicates(fieldName));
            }
        }
        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}
