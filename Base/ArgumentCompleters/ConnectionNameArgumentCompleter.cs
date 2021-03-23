using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PoshCommence.Base
{
    internal class ConnectionNameArgumentCompleter : BaseArgumentCompleter
    {
        // Unlike the fieldnames argument completer, this completer does not necessarily take a parent cateory parameter
        // because in some cases, no category is known
        // In that case, all we want to present the user with is a list of all connection names in Commence
        public override IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName,
           string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            // will return wrong result if ToCategory is specified and that ToCategory category is not the FromCategory (duh)
            // because ContainsKey will try to locate ToCategory
            // we could set up an alternative but it would get really messy really quick.

            //string categoryParam;
            //if (fakeBoundParameters != null && base.ContainsKey(fakeBoundParameters, out categoryParam))
            //{
            //    string categoryKey = fakeBoundParameters[categoryParam].ToString(); // always a string anyway
            //    return CommenceMetadata.Connections[categoryKey]
            //        .Where(s => s.Name.StartsWith(wordToComplete))
            //        .Select(s => new CompletionResult(base.MapString(s.Name)));
            //}
            //else
            //{

            // Instead, we will just show all connectionnames present in the database
            // An interesting side effect is that we *could* now somehow also get the proper ToCategory
            // I haven't explored that yet.
            // Thinking about that aloud, we could possibly work with the ICommenceConnection object instead of 
            // two separate parameters for Connection and ToCategory
            // This would require some rethinking about the CLI experience
            // One way would be to in the CLI show the FullName of the connection, 
            // then in the code behind use the associated ICommenceConnection object.
            // One of the disadvantages of that would be that when a user doe not want to use autocompletion,
            // he would have to type the FullName (and get that right)
            // *or* work with different ParameterSets (both would be quite messy though).
            return CommenceMetadata.Connections.SelectMany(sm => sm.Value) // flatten to list of ienumerables
                    .Select(s => s.Name) // select name from ienumerables
                    .Distinct() // is this necessary?
                    .Where(w => w.StartsWith(wordToComplete)) 
                    .Select(s => new CompletionResult(base.MapString(s)));
            //}
        }
    }
}
