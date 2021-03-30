using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PoshCommence.Base
{
    class ConnectedCategoryArgumentCompleter : BaseArgumentCompleter
    {
        public override IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            string connectionParam;
            if (fakeBoundParameters != null && base.ContainsKey(fakeBoundParameters, base.connectionParams, out connectionParam))
            {
                return CommenceMetadata.Connections
                    .SelectMany(s => s.Value) // select all known connection objext
                    .Where(w => w.Name.Equals(fakeBoundParameters[connectionParam]))
                    .Where(w => w.ToCategory.StartsWith(wordToComplete)) // filter for connectionname
                    .Select(s => new CompletionResult(base.MapString(s.ToCategory))); // return catgorynames that match
            }
            return null;
        }
    }
}
