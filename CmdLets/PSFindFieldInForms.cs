using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;
using PoshCommence.Base;
using System.Windows.Media;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Find, "CmcFieldInForms", SupportsShouldProcess = true)]
    public class PSFindFieldInForms : PSCmdlet
    {
        private IDatabaseSchema schema;

        private string categoryName;
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("c")]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private string fieldName;
        [Parameter(Position = 1, Mandatory = true)]
        [ArgumentCompleter(typeof(FieldNameArgumentCompleter))]
        [Alias("f")]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        protected override void BeginProcessing()
        {
            if (schema is null)
            {
                schema = CommenceMetadata.DatabaseSchema;
            }
        }

        protected override void ProcessRecord()
        {
            var retval = new List<object>();
            
            // we will try locate the provided field(s) in the detail form XML files
            // locate the direct forms
            var forms = schema.Categories.Where(c => c.Name.Equals(categoryName)).SelectMany(s => s.Forms);
            foreach (var o in ParseFormsForField(forms, this.fieldName))
            { 
                    retval.Add(o);
            };
            
            // if the field is the Name field
            // it may be displayed via a connection
            if (schema.Categories.SingleOrDefault(s => s.Name.Equals(categoryName))
                .Fields.SingleOrDefault(s => s.Name.Equals(fieldName))
                .Type.Equals(CommenceFieldType.Name))
            {
                // .. parse all categories
                foreach (var cat in schema.Categories)
                {
                    // .. that have a connection
                    foreach (var con in cat.Connections)
                    {
                        // .. to the category were interested in
                        if (con.ToCategory.Equals(categoryName))
                        {
                            // parse forms in connected category
                            foreach (var o in ParseFormsForField(cat.Forms, this.fieldName))
                            {
                                retval.Add(o);
                            };
                        }
                    }
                }
            }
            WriteObject(retval);
        }

        private IEnumerable<object> ParseFormsForField(IEnumerable<CommenceFormMetaData> forms, string fieldName)
        {
            foreach (var f in forms)
            {
                XElement root = XElement.Parse(f.Xml);
                if (root.Descendants("DATAFIELD").Any(w => w.Value.Equals(fieldName)))
                {
                    // return an anonymous object
                    // note that there is no way to return the caption for a field because
                    // the form XML does not specify a relationship between datafield and caption,
                    // they are just controls with a position
                    yield return new { f.Category, f.Name };
                };
            }
        }
    }
}