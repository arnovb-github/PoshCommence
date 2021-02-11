using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcFilterCTI")]
    public class GetFilterCTI  : PSCmdlet
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
        public string Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        
        private string category;
        [Parameter(Position = 2, Mandatory = true)]
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        
        private string item;
        [Parameter(Position = 3, Mandatory = true)]
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
            ICursorFilterTypeCTI f = new CursorFilterTypeCTI(clauseNumber);
            f.Connection = connection;
            f.Category = category;
            f.Item = item;
            f.Except = except;
            f.OrFilter = orFilter;
            WriteVerbose($"Resulting filterstring is: {f.ToString()}, conjunction is: [{(f.OrFilter ?  "OR" : "AND")}]");
            WriteObject(f, false);
        }
    }
}