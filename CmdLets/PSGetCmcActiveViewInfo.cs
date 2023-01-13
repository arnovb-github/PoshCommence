using PoshCommence.Base;
using System.Management.Automation;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "CmcActiveViewInfo")]
    public class GetCmcActiveViewInfo  : PSCmdlet
    {

        protected override void ProcessRecord()
        {
            var viewInfo = CommenceMetadata.GetActiveViewInfo();
            var responseObject = new PSObject();
            if (viewInfo == null)
            {
                responseObject.Members.Add(new PSNoteProperty("Name", string.Empty));
                responseObject.Members.Add(new PSNoteProperty("Type", string.Empty));
                responseObject.Members.Add(new PSNoteProperty("Category", string.Empty));
                responseObject.Members.Add(new PSNoteProperty("Item", string.Empty));
                responseObject.Members.Add(new PSNoteProperty("Field", string.Empty));
            }
            else
            {
                responseObject.Members.Add(new PSNoteProperty("Name", viewInfo.Name));
                responseObject.Members.Add(new PSNoteProperty("Type", viewInfo.Type));
                responseObject.Members.Add(new PSNoteProperty("Category", viewInfo.Category));
                responseObject.Members.Add(new PSNoteProperty("Item", viewInfo.Item));
                responseObject.Members.Add(new PSNoteProperty("Field", viewInfo.Field));
            }
            WriteObject(responseObject);
        }
    }
}