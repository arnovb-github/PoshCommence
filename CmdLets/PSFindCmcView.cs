using System;
using System.Management.Automation;
using System.Collections.Generic;
using Vovin.CmcLibNet.Database.Metadata;
using System.Linq;
using System.Collections.ObjectModel;
using PoshCommence.Base;

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Find, "CmcView")]
    public class FindCmcView : PSCmdlet
    {

        private string viewName;
        [Parameter(Position = 0)]
        public string Name
        {
            get { return viewName; }
            set { viewName = value; }
        }

        private string categoryName;
        [Parameter]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        public string Category
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private string viewType;
        [Parameter]
        [ArgumentCompleter(typeof(ViewTypeArgumentCompleter))]
        public string Type
        {
            get { return viewType; }
            set { viewType = value; }
        }

        private bool force;
        [Parameter]
        public SwitchParameter Force
        {
            get { return force; }
            set { force = value; }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

        }
        protected override void ProcessRecord()
        {
            if (!ViewInfo.Ready() || force) {
                ViewInfo.GetViewDefinitions();
            }
            WriteObject(ViewInfo.FindView(CreateFilterList()));
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        private List<Func<IViewDef, bool>> CreateFilterList()
        {
            var filters = new List<Func<IViewDef, bool>>();
            if (!string.IsNullOrEmpty(viewName)) { filters.Add(s => !string.IsNullOrWhiteSpace(s.Name) && s.Name.Contains(viewName)); }
            if (!string.IsNullOrEmpty(categoryName)) { filters.Add(s => s.Category.ToLower().Contains(categoryName.ToLower())); }
            if (!string.IsNullOrEmpty(viewType)) { filters.Add(s => s.Type.ToLower().Contains(viewType.ToLower())); }
            return filters;
        }
    }
}