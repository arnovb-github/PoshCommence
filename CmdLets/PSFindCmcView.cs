using System;
using System.Management.Automation;
using System.Collections.Generic;
using Vovin.CmcLibNet.Database.Metadata;
using PoshCommence.Base;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Find, "CmcView")]
    public class FindCmcView : PSCmdlet
    {

        private string viewName;
        [Parameter(Position = 0)]
        [ArgumentCompleter(typeof(ViewNameArgumentCompleter))]
        public string Name
        {
            get { return viewName; }
            set { viewName = value; }
        }

        private string categoryName;
        [Parameter]
        [Alias("c")]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private string viewType;
        [Parameter]
        [Alias("t")]
        [ArgumentCompleter(typeof(ViewTypeArgumentCompleter))]
        public string Type
        {
            get { return viewType; }
            set { viewType = value; }
        }

        //private bool force;
        //[Parameter]
        //[Alias("f")]
        //public SwitchParameter Force
        //{
        //    get { return force; }
        //    set { force = value; }
        //}

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

        }
        protected override void ProcessRecord()
        {
            WriteObject(CommenceMetadata.FindView(CreateFilterList()));
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        private List<Func<IViewDef, bool>> CreateFilterList()
        {
            var filters = new List<Func<IViewDef, bool>>();
            if (!string.IsNullOrEmpty(viewName)) { filters.Add(s => !string.IsNullOrWhiteSpace(s.Name) && s.Name.ToLower().Contains(viewName.ToLower())); }
            if (!string.IsNullOrEmpty(categoryName)) { filters.Add(s => s.Category.ToLower().Contains(categoryName.ToLower())); }
            if (!string.IsNullOrEmpty(viewType)) { filters.Add(s => s.Type.ToLower().Contains(viewType.ToLower())); }
            return filters;
        }
    }
}