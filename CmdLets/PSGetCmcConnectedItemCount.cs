using System.Collections.Generic;
using System.Management.Automation;
using PoshCommence.Base;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{

    [Cmdlet(VerbsCommon.Get, "CmcConnectedItemCount")]
    public class GetCmcConnectedItemCount : PSCmdlet
    {
        private string fromCategory;
        [Parameter(Position = 0, Mandatory = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        public string FromCategory
        {
        get { return fromCategory; }
        set { fromCategory = value; }
        }
        private string connectionName;
        [Parameter(Position = 1, Mandatory = true)]
        public string ConnectionName
        {
            get { return connectionName; }
            set { connectionName = value; }
        }

        private string toCategory;
        [Parameter(Position = 2, Mandatory = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        public string ToCategory
        {
        get { return toCategory; }
        set { toCategory = value; }
        }

        // value of the item to count connections for
        // this parameter is optional
        private string fromItem;
        [Parameter(Position = 3)]
        public string FromItem
        {
            get { return fromItem; }
            set { fromItem = value; }
        }

        private string clarifySeparator;
        [Parameter(Position = 4)]
        public string ClarifySeparator
        {
            get { return clarifySeparator; }
            set { clarifySeparator = value; }
        }

        private string clarifyValue;
        [Parameter(Position = 5)]
        public string ClarifyValue
        {
            get { return clarifyValue; }
            set { clarifyValue = value; }
        }
        
        protected override void ProcessRecord()
        {
            var db = new CommenceDatabase();
            string result = db.ViewCategory(fromCategory); // if fromCategory is not found, nothing happens
            if (result.ToLower() != "ok") {
                WriteError(new ErrorRecord(new Vovin.CmcLibNet.CommenceDDEException(result),
                    "CategoryNotFound",
                    ErrorCategory.InvalidResult,
                    db));
                return;
            }
            string clarifyState = db.ClarifyItemNames();
            try {
                int connectedItemCount = -1;
                // no item supplied, process entire category
                if (string.IsNullOrEmpty(fromItem))
                {
                    int numItems = db.GetItemCount(fromCategory);
                    List<string> itemNames = db.GetItemNames(fromCategory);
                    db.ClarifyItemNames("TRUE");
                    for (int i = 1; i <= numItems; i++) // DDE-call indexes in Commence are 1-based
                    {
                        // since this not rely on string values, it should be reliable
                        connectedItemCount = db.ViewConnectedCount(i, connectionName, toCategory);
                        WriteObject(new
                            {
                                ItemName = itemNames[i - 1],
                                FromCategory = fromCategory,
                                Connection = connectionName,
                                ToCategory = toCategory,
                                Count = connectedItemCount
                            },
                            false); // return and do not enumerate. I.e. pass every object separately.
                    }
                }
                else
                { 
                    if (!string.IsNullOrEmpty(clarifyValue) && !string.IsNullOrEmpty(clarifySeparator)) 
                    {
                        fromItem = db.GetClarifiedItemName(fromItem, clarifySeparator, clarifyValue);
                        db.ClarifyItemNames("TRUE");
                    }
                    connectedItemCount = db.GetConnectedItemCount(fromCategory, fromItem, connectionName, toCategory);
                    WriteObject(new
                        {
                            ItemName = fromItem,
                            FromCategory = fromCategory,
                            Connection = connectionName,
                            ToCategory = toCategory,
                            Count = connectedItemCount
                        },
                        false);
                }
                if (connectedItemCount == -1) {
                    WriteError(new ErrorRecord(new Vovin.CmcLibNet.CommenceDDEException("Vovin.CmcLibNet was unable to get a valid result from Commence."),
                        "CommenceDDEError",
                        ErrorCategory.InvalidArgument,
                        db));

                    WriteVerbose("A count of -1 means an error occurred while trying to receive the count from Commence.\n"
                        + "This is likely caused by one or more of the arguments being invalid.\n"
                        + "Note that connection- and viewnames in Commence are case-sensitive!\n\n"
                        + "A clarify separator may include spaces. Make sure to include them in the command.\n\n");
                    return;
                }
            }
            finally
            {
                db.ClarifyItemNames(clarifyState); // restore state
                db.Close();
            }
        }
    }
}