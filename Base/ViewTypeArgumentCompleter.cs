using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Collections.Generic;
using System.Collections;

namespace PoshCommence.Base
{
    internal class ViewTypeArgumentCompleter : BaseArgumentCompleter
    {
        public override IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, 
            string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            // IDictionary fakeBoundParameters is like @PSBoundParameters
            // https://ss64.com/ps/psboundparameters.html
            return ViewInfo.ViewTypes.Where(s => s.ToLower().StartsWith(wordToComplete.ToLower()))
               .Select(s => new CompletionResult(base.MapString(s)));
        }
    }
}