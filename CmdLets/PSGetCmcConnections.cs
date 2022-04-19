using System.Management.Automation;
using System.Collections.Generic;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;
using PoshCommence.Base;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcConnections")]
    public class GetCmcConnections : PSCmdlet
    {
        private string categoryName;
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [Alias("c")]  
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        protected override void ProcessRecord()
        {
            foreach (var o in GetConnections(categoryName))
            {
                WriteObject(o);
            }
        }

        private IEnumerable<ICommenceConnection> GetConnections(string categoryName) 
        {
            using (ICommenceDatabase db = new CommenceDatabase())
            {
                return db.GetConnectionNames(categoryName);
            }
        }
    }
}