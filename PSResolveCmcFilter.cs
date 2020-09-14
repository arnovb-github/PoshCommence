using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PSCommenceModules
{
    // try to apply a filter.
    // this is an expensive operation.
    // it is recommended you not use this in production code.
    [Cmdlet(VerbsDiagnostic.Resolve, "CmcFilter")]
    public class ResolveCmcFilter  : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Category { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public ICursorFilter Filter { get; set; }

        protected override void ProcessRecord()
        {
            bool retval;
            retval = CursorFilters.ValidateFilter(Category, Filter);
            WriteObject(retval);
            if (retval)
            {
                WriteVerbose($"Succesfully applied {Filter} on category {Category}");
            }
            else
            {
                WriteVerbose($"Filter '{Filter}' could not be applied to category '{Category}'. Check the spelling of the category- and fieldname(s), and make sure filterqualifier can be applied to the fieldtypes you provided.");
            }
        }
    }
}