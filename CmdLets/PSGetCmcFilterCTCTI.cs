using System.Management.Automation;
using PoshCommence.Base;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcFilterCTCTI")]
    public class GetFilterCTCTI  : PSCmdlet
    {
        private int clauseNumber;
        [Parameter(Position = 0, Mandatory = true)]
        public int ClauseNumber
        {
            get { return clauseNumber; }
            set { clauseNumber = value; }
        }

        private string connection;
        [Parameter(Position = 1, Mandatory = true)]
        [ArgumentCompleter(typeof(ConnectionNameArgumentCompleter))]
        public string Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        
        private string category;
        [Parameter(Position = 2, Mandatory = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [Alias("c")]        
        public string CategoryName
        {
            get { return category; }
            set { category = value; }
        }

        private string connection2;
        [Parameter(Position = 3, Mandatory = true)]
        [ArgumentCompleter(typeof(ConnectionNameArgumentCompleter))]
        public string Connection2
        {
            get { return connection2; }
            set { connection2 = value; }
        }

        private string toCategoryName;
        [Parameter(Position = 4, Mandatory = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        public string ToCategoryName
        {
            get { return toCategoryName; }
            set { toCategoryName = value; }
        }
        
        private string item;
        [Parameter(Position = 5, Mandatory = true)]
        public string Item
        {
            get { return item; }
            set { item = value; }
        }

        private bool except;
        [Parameter()]
        public SwitchParameter Except
        {
            get { return except; }
            set { except = value; }
        }
   
        private bool orFilter;
        [Parameter()]
        public SwitchParameter OrFilter
        {
            get { return orFilter; }
            set { orFilter = value; }
        }
        protected override void ProcessRecord()
        {
            ICursorFilterTypeCTCTI f = new CursorFilterTypeCTCTI(clauseNumber);
            f.Connection = connection;
            f.Category = category;
            f.Connection2 = connection2;
            f.Category2 = toCategoryName;
            f.Item = item;
            f.Except = except;
            f.OrFilter = orFilter;
            WriteVerbose($"Resulting filterstring is: {f}, conjunction is: [{(f.OrFilter ?  "OR" : "AND")}]");
            WriteObject(f, false);
        }
    }
}