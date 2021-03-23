using System.Management.Automation;
using System.Management.Automation.Language;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace PoshCommence.Base
{
    internal abstract class BaseArgumentCompleter : IArgumentCompleter
    {
        // a list of valid parameternamews that specify a category
        // we need more than one, because sometimes more than one is specified,
        // or it is has a different name
        protected readonly string[] categoryParameterNames = {"CategoryName", "ToCategoryName"};
        protected internal string MapString(string s)
        {
            if (s.Contains(" "))
                return $"\"{s}\"";
            return s;
        }

        protected internal bool ContainsKey(IDictionary dict, out string result)
        {
            result = string.Empty;
            var matches = dict.Keys.Cast<string>().ToArray().Intersect(categoryParameterNames);
            // when more than 1 parameter matches, we have no mechanism of telling which one to use
            // i.e. if we'd have a command that specifies both the -CategoryName and -ToCategoryName parameters
            // the CLI would not know whether to auto-complete -CategoryName of -ToCategoryName
            // A workaround for this could be to work with dynamically generated ValidateSets,
            // but that is only implemented in PS 7, not sure if (or how) we can do that.
            if (matches.Count() > 1) { return false; }
            result = matches.FirstOrDefault();
            return !string.IsNullOrEmpty(result);
        }

        // returns a specific key, but how to get the searchKey from the CLI?
        // Powershell will just pass in the entire parameter dictionary
        protected internal bool ContainsKey(IDictionary dict, string searchKey, out string result)
        {
            result = dict.Keys.Cast<string>().FirstOrDefault(f => f.Equals(searchKey));
            return !string.IsNullOrEmpty(result);
        }

        /* 
        When Powershell calls the completer, the fakeBoundParameters is populated
        with the parameter names and  parameters values passed to the command.
        For example, "Do-Stuff -Foo bar" will have 'Foo' as key and 'bar' as value.
        fakeBoundParameters is equivalent to $PSBoundParameters
        */
        public abstract IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, 
            string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters);
    }
}