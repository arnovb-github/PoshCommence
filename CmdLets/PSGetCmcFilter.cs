using PoshCommence.Base.DynamicParameters;
using PoshCommence.Base.Extensions;
using System.Management.Automation;
using Vovin.CmcLibNet.Attributes;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{
    /* A dynamic parameter approach to getting filters
     * This works perfectly, but discoverability of the parameters is a problem
     * Get-Command Get-CmcFilter will default to whatever is the default value for FilterType
     * In this case it is the syntax for enum value 0 in FilterType.
     */
    [Cmdlet(VerbsCommon.Get, "CmcFilter")]
    public class GetFilter : PSCmdlet, IDynamicParameters
    {
        private int clauseNumber;
        [Parameter(Position = 0, Mandatory = true)]
        [ValidateSet("1","2","3","4","5","6","7","8")]
        public int ClauseNumber
        {
            get { return clauseNumber; }
            set { clauseNumber = value; }
        }

        private FilterType _type;
        [Parameter(Position = 1, Mandatory = true)]
        [Alias("t", "Type")]
        public FilterType FilterType
        {
            get { return _type; }
            set { _type = value; }
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
            // TODO: should we include a check for checking the Qualifier and chosen fieldtype?
            ICursorFilter filter;
            switch (_type)
            {
                case FilterType.Field:
                    GetCmcFilterCommandDynamicParametersF ctxF = context as GetCmcFilterCommandDynamicParametersF;
                    var f = new CursorFilterTypeF(clauseNumber)
                    {
                        FieldName = ctxF.FieldName,
                        Qualifier = ctxF.Qualifier,
                        MatchCase = ctxF.MatchCase
                    };
                    if (f.Qualifier == FilterQualifier.Between)
                    {
                        f.FilterBetweenStartValue = ctxF.FieldValue;
                        f.FilterBetweenEndValue = ctxF.FieldValue2;
                    }
                    if (f.Qualifier.GetAttribute<FilterValuesAttribute>()?.Number == 1)
                    {
                        f.FieldValue = ((GetCmcFilterCommandDynamicParametersF)context).FieldValue;
                    }
                    filter = f;
                    break;
                case FilterType.ConnectionToItem:
                    GetCmcFilterCommandDynamicParametersCTI ctxCti = context as GetCmcFilterCommandDynamicParametersCTI;
                    var cti = new CursorFilterTypeCTI(clauseNumber)
                    {
                        Connection = ctxCti.Connection,
                        Category = ctxCti.ToCategoryName,
                        Item = ctxCti.Item
                    };
                    filter = cti;
                    break;

                case FilterType.ConnectionToCategoryField:
                    GetCmcFilterCommandDynamicParametersCTCF ctxCtcf = context as GetCmcFilterCommandDynamicParametersCTCF;
                    var ctcf = new CursorFilterTypeCTCF(clauseNumber)
                    {
                        Connection = ctxCtcf.Connection,
                        Category = ctxCtcf.ToCategoryName,
                        FieldName = ctxCtcf.FieldName,
                        Qualifier = ctxCtcf.Qualifier,
                        MatchCase = ctxCtcf.MatchCase
                    };
                    if (ctcf.Qualifier == FilterQualifier.Between)
                    {
                        ctcf.FilterBetweenStartValue = ctxCtcf.FieldValue;
                        ctcf.FilterBetweenEndValue = ctxCtcf.FieldValue2;
                    }
                    if (ctcf.Qualifier.GetAttribute<FilterValuesAttribute>()?.Number == 1)
                    {
                        ctcf.FieldValue = ctxCtcf.FieldValue;
                    }
                    filter = ctcf;
                    break;

                case FilterType.ConnectionToCategoryToItem:
                    GetCmcFilterCommandDynamicParametersCTCTI ctxCtcti = context as GetCmcFilterCommandDynamicParametersCTCTI;
                    ICursorFilterTypeCTCTI ctcti = new CursorFilterTypeCTCTI(clauseNumber)
                    {
                        Connection = ctxCtcti.Connection,
                        Category = ctxCtcti.ToCategoryName,
                        Connection2 = ctxCtcti.Connection2,
                        Category2 = ctxCtcti.ToCategoryName2,
                        Item = ctxCtcti.Item
                    };
                    filter = ctcti;
                    break;
                default:
                    throw new System.ArgumentException("Invalid or missing FilterType");
            }
            filter.Except = this.Except;
            filter.OrFilter = this.OrFilter;
            WriteVerbose($"Resulting filterstring is: {filter}, conjunction is: [{(filter.OrFilter ? "OR" : "AND")}]");
            WriteObject(filter, false);
        }

        
        private object context;
        public object GetDynamicParameters()
        {
            switch (_type)
            {
                case FilterType.Field:
                    context = new GetCmcFilterCommandDynamicParametersF();
                    return context;
                case FilterType.ConnectionToItem:
                    context = new GetCmcFilterCommandDynamicParametersCTI();
                    return context;
                case FilterType.ConnectionToCategoryField:
                    context = new GetCmcFilterCommandDynamicParametersCTCF();
                    return context;
                case FilterType.ConnectionToCategoryToItem:
                    context = new GetCmcFilterCommandDynamicParametersCTCTI();
                    return context;
                default:
                    throw new System.ArgumentException("Invalid or missing FilterType"); ;
            }
        }
    }
}
