using System.Management.Automation;
using PoshCommence.Base;

namespace PoshCommence.CmdLets
{
    // TODO: I'm not sure about the name of this cmdlet
    // It does 'get' something, but its usage is to 'set' something
    [Cmdlet(VerbsCommon.Get, "CmcConnectedField")]
    public class GetCmcConnectedField : PSCmdlet
    {
        private string connectionName;
        [Parameter(Position = 0, Mandatory = true)]
        [ArgumentCompleter(typeof(ConnectionNameArgumentCompleter))]
        [Alias("cn")]
        public string ConnectionName
        {
            get { return connectionName; }
            set { connectionName = value; }
        }

        private string toCategoryName;
        [Parameter(Position = 1, Mandatory = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [Alias("c")]         
        public string ToCategoryName
        {
            get { return toCategoryName; }
            set { toCategoryName = value; }
        }
        
        private string fieldName;
        [Parameter(Position = 2, Mandatory = true)]
        [ArgumentCompleter(typeof(FieldNameArgumentCompleter))]
        [Alias("f")]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        protected override void ProcessRecord()
        {
            var rc = new ConnectedField(connectionName, toCategoryName, fieldName);
            WriteObject(rc);
        }
    }
}