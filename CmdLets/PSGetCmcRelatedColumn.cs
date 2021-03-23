using System.Management.Automation;
using PoshCommence.Base;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcRelatedColumn")]
    public class GetCmcRelatedColumn : PSCmdlet
    {
        private string connectionName;
        [Parameter(Position = 0, Mandatory = true)]
        public string ConnectionName
        {
            get { return connectionName; }
            set { connectionName = value; }
        }

        private string toCategory;
        [Parameter(Position = 1, Mandatory = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [Alias("c")]         
        public string ToCategory
        {
            get { return toCategory; }
            set { toCategory = value; }
        }
        
        private string fieldName;
        [Parameter(Position = 2, Mandatory = true)]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        protected override void ProcessRecord()
        {
            var rc = new RelatedColumn(connectionName, toCategory, fieldName);
            WriteObject(rc);
        }
    }
}