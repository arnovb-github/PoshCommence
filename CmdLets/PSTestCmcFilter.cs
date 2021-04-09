using System.Management.Automation;
using PoshCommence.Base;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{
    // try to apply a filter.
    // This is an expensive operation,
    // you should not use this in production code.
    [Cmdlet(VerbsDiagnostic.Test, "CmcFilter")]
    public class TestCmcFilter  : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [Alias("c")]         
        public string Category { get; set; }

        [Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
        public ICursorFilter Filter { get; set; }

        protected override void ProcessRecord()
        {
            using (var db = new CommenceDatabase())
            using (var cur = db.GetCursor(Category)) // this may also fail
            {
                int total = cur.RowCount;
                cur.Filters.Add(Filter);
                try
                {
                    cur.Filters.Apply();
                }
                catch
                {
                    WriteVerbose($"Could not apply filter '{Filter}' to category '{Category}'. Check the spelling of the category- and fieldname(s) and make sure filterqualifier can be applied to the fieldtypes you provided.");
                    WriteObject(false);
                    return;
                }
                WriteVerbose($"Succesfully applied filter '{Filter}' to category '{Category}'. {cur.RowCount} of {total} items returned.");
                WriteObject(true);
            }
        }
    }
}