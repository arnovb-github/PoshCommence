using System.Management.Automation;
using System.Collections.Generic;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcCategories")]
    public class GetCmcCategories  : PSCmdlet
    {
        private IEnumerable<object> GetCategories() {
            using (ICommenceDatabase db = new CommenceDatabase())
            {
                var categories = db.GetCategoryNames();
                foreach (string c in categories)
                {
                    ICategoryDef def = db.GetCategoryDefinition(c);
                    yield return new { CategoryName = c,
                        Id = def.CategoryID,
                        Clarified = def.Clarified,
                        ClarifyField = def.ClarifyField,
                        ClarifySeparator = def.ClarifySeparator,
                        Duplicates = def.Duplicates,
                        MaxItems = def.MaxItems,
                        Shared = def.Shared
                    };
                }
            }
        }

        protected override void ProcessRecord()
        {
            foreach (var cdef in GetCategories())
            {
                WriteObject(cdef);
            }
        }
    }
}