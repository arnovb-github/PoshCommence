using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Collections.Generic;
using System.Collections;

namespace PoshCommence.Base
{
    internal class CategoryNameArgumentCompleter : BaseArgumentCompleter
    {
        public override IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, 
            string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            return ViewInfo.Categories.Where(s => s.ToLower().StartsWith(wordToComplete.ToLower()))
               .Select(s => new CompletionResult(base.MapString(s)));
        }
    }
}