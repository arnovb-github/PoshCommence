using System;
using System.Management.Automation;
using System.Collections.Generic;
using Vovin.CmcLibNet.Database.Metadata;
using System.Linq;
using System.Collections.ObjectModel;
using PoshCommence.Base;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsDiagnostic.Test, "Dynamic")]
    public class TestDynamic : PSCmdlet, IDynamicParameters
    {
        private string x;
        [Parameter(Position = 0)]
        public string X
        {
            get { return x; }
            set { x = value; }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }
        protected override void ProcessRecord()
        {
            WriteObject("just testing.");
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        private List<string> param1Values = new List<string>() { "Hello", "World"};
        private List<string> param2Values = new List<string>() { "Foo", "Bar"};
     
        public object GetDynamicParameters()
        {
            var dict = new RuntimeDefinedParameterDictionary();
            var ParamA = new PowershellDynamicParameter(this, "ParamA", param1Values.ToArray())
            {
                ParameterSetName = "ParamASet",
                AllowMultiple = true,
                Position = 1,
                Mandatory = false
            };
            ParamA.AddToParamDictionary(dict);
            var ParamB = new PowershellDynamicParameter(this, "ParamB", param2Values.ToArray())
            {
                ParameterSetName = "ParamBSet",
                AllowMultiple = true,
                Position = 2,
                Mandatory= false
            };
            ParamB.AddToParamDictionary(dict);
            return dict;
        }
    }
}