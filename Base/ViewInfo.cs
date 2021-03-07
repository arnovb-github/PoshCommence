using System;
using System.Collections.Generic;
using System.Linq;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;

namespace PoshCommence.CmdLets
{

    internal static class ViewInfo
    {
        private static List<IViewDef> viewList = new List<IViewDef>();

        internal static List<string> Categories {get; private set;}
        internal static IEnumerable<IViewDef> FindView(List<Func<IViewDef, bool>> filters)
        {
            if (filters is null) { return viewList; }
            IEnumerable<IViewDef> retval = viewList;
            foreach (var f in filters)
            {
                retval = retval.Where(f);
            }
            return retval;
        }

        internal static bool Ready() => viewList.Any();

        internal static void GetViewDefinitions()
        {
            using (var db = new CommenceDatabase()) {
                Categories = db.GetCategoryNames();
                foreach (string categoryName in Categories) {
                    foreach (string viewName in db.GetViewNames(categoryName)) {
                        viewList.Add(db.GetViewDefinition(viewName));
                    }
                }
            }
        }

    }
}