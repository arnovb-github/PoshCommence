using PoshCommence.Base;
using System.Management.Automation;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.CmdLets
{

    [Cmdlet(VerbsCommon.Get, "CmcFilterQualifiers")]
    public class GetCmcFilterQualifiers : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            foreach (var o in EnumHelper.ListEnum<FilterQualifier>())
            {
                WriteObject(o);
            }
        }
    }
}