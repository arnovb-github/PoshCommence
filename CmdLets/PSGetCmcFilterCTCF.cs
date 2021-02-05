using PSCommenceModules.Base.Extensions;
using System.Management.Automation;
using Vovin.CmcLibNet.Attributes;
using Vovin.CmcLibNet.Database;

namespace PSCommenceModules.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcFilterCTCF")]
    public class GetFilterCTCF  : PSCmdlet
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
        private string fieldName;
        [Parameter(Position = 3, Mandatory = true)]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        private FilterQualifier qualifier;
        [Parameter(Position = 4, Mandatory = true)]
        public FilterQualifier Qualifier
        {
            get { return qualifier; }
            set { qualifier = value; }
        }
        private string fieldValue;
        [Parameter(Position = 5, Mandatory = true)]
        public string FieldValue
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }
        
        private string fieldValue2;
        [Parameter(Position = 6, Mandatory = false)]
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
            ICursorFilterTypeCTCF f = new CursorFilterTypeCTCF(clauseNumber);
            f.Except = except;
            f.Connection = connection;
            f.Category = category;
            f.FieldName = fieldName;
            f.Qualifier = qualifier;
            if (qualifier == FilterQualifier.Between)
            {
                f.FilterBetweenStartValue = fieldValue;
                f.FilterBetweenEndValue = fieldValue2;
            }
            if (qualifier.GetAttribute<FilterValuesAttribute>()?.Number == 1)
            {
                f.FieldValue = fieldValue;
            }
            f.MatchCase = matchCase;
            f.OrFilter = orFilter;
            WriteVerbose($"Resulting filterstring is: {f.ToString()}, conjunction is: [{(f.OrFilter ?  "OR" : "AND")}]");
            WriteObject(f, false);
        }
    }
}