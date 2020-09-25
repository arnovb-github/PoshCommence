using System.Collections.Generic;
using System.Management.Automation;

namespace PSCommenceModules
{

    [Cmdlet(VerbsCommon.Get, "CmcFieldValue")]
    public class GetCmcField : PSCmdlet
    {
        private string categoryName;
        [Parameter(Position = 0, Mandatory = true)]
        public string CategoryOrViewName
        {
        get { return categoryName; }
        set { categoryName = value; }
        }
        private string fieldName;
        [Parameter(Position = 1, Mandatory = true)]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        // use this to pass in a viewname
        private bool useView;
        [Parameter()]
        public SwitchParameter UseView
        {
            get { return useView; }
            set { useView = value; }
        }

        protected override void ProcessRecord()
        {
            // do the data reading
            var rows = Helper.GetCmcFieldValues(categoryName,
                new List<string>() { fieldName },
                null,
                null,
                useView,
                false);

            foreach (var row in rows)
            {
                WriteObject(row[0], false); // return and do not enumerate. I.e. pass every row separately.
            }
        }
    }
}