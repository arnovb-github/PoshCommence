using System;
using System.Management.Automation;
using System.Text;
using Vovin.CmcLibNet.Database;

namespace PSCommenceModules
{

    [Cmdlet(VerbsCommon.Show, "CmcFilterQualifiers")]
    public class ShowCmcFilterQualifiers : PSCmdlet
    {

        protected override void ProcessRecord()
        {
            
            foreach (FilterQualifier q in (FilterQualifier[]) Enum.GetValues(typeof(FilterQualifier)))
            {
                StringBuilder sb = new StringBuilder("[");
                sb.Append(typeof(FilterQualifier).Namespace);
                sb.Append("]::");
                sb.Append(q.ToString());
                WriteObject(new { Qualifier = sb.ToString(), Value = (int)q });
            }
        }
    }
}