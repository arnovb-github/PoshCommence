using System.Management.Automation;
using System.Management.Automation.Language;
using System.Collections.Generic;
using System.Collections;

namespace PoshCommence.Base
{
    internal abstract class BaseArgumentCompleter : IArgumentCompleter
    {
        protected internal string MapString(string s)
        {
            if (s.Contains(" "))
                return $"\"{s}\"";
            return s;
        }
        public abstract IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, 
            string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters);
    }
}