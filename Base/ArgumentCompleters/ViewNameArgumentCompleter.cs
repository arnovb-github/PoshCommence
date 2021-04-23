using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Collections.Generic;
using System.Collections;

namespace PoshCommence.Base
{
    internal class ViewNameArgumentCompleter : BaseArgumentCompleter
    {
        public override IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, 
            string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            return CommenceMetadata.ViewNames.Keys.Where(s => s.StartsWith(wordToComplete))
                .Select(s => new CompletionResult(base.MapString(s)));
        }
    }
}