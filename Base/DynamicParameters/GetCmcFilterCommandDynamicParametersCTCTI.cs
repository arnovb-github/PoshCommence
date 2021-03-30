using System.Management.Automation;

namespace PoshCommence.Base.DynamicParameters
{
    internal class GetCmcFilterCommandDynamicParametersCTCTI
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
        //[ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [ArgumentCompleter(typeof(ConnectedCategoryArgumentCompleter))]
        public string ToCategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private string connection2;
        [Parameter(Position = 4, Mandatory = true)]
        [ArgumentCompleter(typeof(ConnectionNameArgumentCompleter))]
        public string Connection2
        {
            get { return connection2; }
            set { connection2 = value; }
        }

        private string toCategoryName2;
        [Parameter(Position = 5, Mandatory = true)]
        [ArgumentCompleter(typeof(ConnectedCategoryArgumentCompleter))]
        public string ToCategoryName2
        {
            get { return toCategoryName2; }
            set { toCategoryName2 = value; }
        }

        private string item;
        [Parameter(Position = 6, Mandatory = true)]
        public string Item
        {
            get { return item; }
            set { item = value; }
        }
    }
}
