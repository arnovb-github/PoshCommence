using System.Collections.Generic;
using System.Management.Automation;

namespace PSCommenceModules
{

    [Cmdlet(VerbsCommon.Get, "CmcConnectedItemCount")]
    public class CmcConnectedItemCount : PSCmdlet
    {
        private string fromCategory;
        [Parameter(Position = 0, Mandatory = true)]
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
        
        protected override void ProcessRecord()
        {
            var db = new Vovin.CmcLibNet.Database.CommenceDatabase();
            string clarifyState = db.ClarifyItemNames();
            int connectedItemCount;
            try {
                if (string.IsNullOrEmpty(fromItem))
                {
                    int numItems = db.GetItemCount(fromCategory);
                    for (int i = 1; i <= numItems; i++) // DDE-call indexes in Commence are 1-based
                    {
                        db.ViewCategory(fromCategory);
                        db.ClarifyItemNames("TRUE");
                        List<string> itemNames = db.GetItemNames(fromCategory);
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
                    // We received a specific item to test for.
                    // Depending on how crazy its values, we may get -1, 
                    // meaning Commence could not handle it.
                    // A typical example would be 'Earvin "Magic" Johnson', 
                    // that will choke the DDE call on the inner quotes.
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
            }
            finally
            {
                db.ClarifyItemNames(clarifyState); // restore state
                db.Close();
            }
        }
    }
}