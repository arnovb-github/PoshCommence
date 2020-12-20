using PSCommenceModules.Base;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PSCommenceModules.CmdLets
{

    [Cmdlet(VerbsCommon.Show, "CmcFieldTypes")]
    public class ShowCmcCmcFieldTypes : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            foreach (var o in EnumHelper.ListEnum<CommenceFieldType>())
            {
                WriteObject(o);
            }
        }
    }
}