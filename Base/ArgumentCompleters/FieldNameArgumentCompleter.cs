using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PoshCommence.Base
{
    internal class FieldNameArgumentCompleter : BaseArgumentCompleter
    {
        public override IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, 
            string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            string categoryParam;
            if (fakeBoundParameters != null && base.ContainsKey(fakeBoundParameters, base.categoryParameterNames, out categoryParam))
            {
                // we want to search in the FieldNames dictionary in the static 'cache' class 
                // the (list of) values that belong to the key that equals 
                // the value of the key with name "CategoryName" in fakeBoundParameters
                string categoryKey = fakeBoundParameters[categoryParam].ToString(); // always a string anyway
                return CommenceMetadata.FieldNames(categoryKey).Where(s => s.ToLower().StartsWith(wordToComplete.ToLower()))
                    .Select(s => new CompletionResult(base.MapString(s)));
            }
            return null;
        }
    }
}