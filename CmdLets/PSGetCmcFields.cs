using System.Management.Automation;
using System.Collections.Generic;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;

namespace PSCommenceModules.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcFields")]
    public class GetCmcFields  : PSCmdlet
    {
        private string categoryName;
        [Parameter(Position = 0, Mandatory = true)]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        protected override void ProcessRecord()
        {
            foreach (var o in GetFields(categoryName))
            {
                WriteObject(o);
            }
        }

        private IEnumerable<object> GetFields(string categoryName) 
        {
            using (ICommenceDatabase db = new CommenceDatabase())
            {
                var fieldNames = db.GetFieldNames(categoryName);
                foreach (string fname in fieldNames)
                {
                    ICommenceFieldDefinition fdef = db.GetFieldDefinition(categoryName, fname);
                    yield return new { Name = fname,
                        Combobox = fdef.Combobox,
                        DefaultString = fdef.DefaultString,
                        Mandatory = fdef.Mandatory,
                        MaxChars = fdef.MaxChars,
                        Recurring = fdef.Recurring,
                        Shared = fdef.Shared,
                        Type = fdef.Type
                    };
                }
            }
        }
    }
}