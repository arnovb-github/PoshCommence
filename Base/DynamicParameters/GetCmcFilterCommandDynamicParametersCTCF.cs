using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.Base.DynamicParameters
{
    internal class GetCmcFilterCommandDynamicParametersCTCF
    {
        private string connection;
        [Parameter(Position = 2, Mandatory = true)]
        [ArgumentCompleter(typeof(ConnectionNameArgumentCompleter))]
        public string Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        private string categoryName;
        [Parameter(Position = 3, Mandatory = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        public string ToCategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private string fieldName;
        [Parameter(Position = 4, Mandatory = true)]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        private FilterQualifier qualifier;
        [Parameter(Position = 5, Mandatory = true)]
        public FilterQualifier Qualifier
        {
            get { return qualifier; }
            set { qualifier = value; }
        }
        private string fieldValue;
        [Parameter(Position = 6, Mandatory = true)]
        public string FieldValue
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        private string fieldValue2;
        [Parameter(Position = 7, Mandatory = false)]
        public string FieldValue2
        {
            get { return fieldValue2; }
            set { fieldValue2 = value; }
        }

        private bool matchCase;
        [Parameter()]
        public SwitchParameter MatchCase
        {
            get { return matchCase; }
            set { matchCase = value; }
        }
    }
}
