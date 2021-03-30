using System;
using System.Linq;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.Base.DynamicParameters
{
    internal  class GetCmcFilterCommandDynamicParametersF
    {
        // we cannot autocomplete here because we do not know
        // what category we are working in
        private string fieldName;
        // specifying the parametersetname is overkill but it doesn't hurt either
        [Parameter(Position = 2, Mandatory = true)]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        private FilterQualifier qualifier;
        [Parameter(Position = 3, Mandatory = true)]
        public FilterQualifier Qualifier
        {
            get { return qualifier; }
            set { qualifier = value; }
        }

        private string fieldValue;
        [Parameter(Position = 4, Mandatory = true)]
        public string FieldValue
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        private string fieldValue2;
        [Parameter(Position = 5, Mandatory = false)]
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
