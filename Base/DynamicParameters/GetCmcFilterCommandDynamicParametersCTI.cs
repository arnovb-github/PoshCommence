using System.Management.Automation;

namespace PoshCommence.Base.DynamicParameters
{
    internal class GetCmcFilterCommandDynamicParametersCTI
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

        private string item;
        [Parameter(Position = 4, Mandatory = true)]
        public string Item
        {
            get { return item; }
            set { item = value; }
        }
    }
}
