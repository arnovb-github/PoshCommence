using System;
using System.Collections.Generic;
using System.Linq;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;

namespace PoshCommence.Base
{
   internal static class ViewInfo
    {
        private static List<IViewDef> viewList = new List<IViewDef>();
        private static IEnumerable<string> viewTypes;
        private static IEnumerable<string> categoryNames;

        internal static IEnumerable<string> Categories => categoryNames;
        internal static IEnumerable<IViewDef> Views => viewList;
        internal static IEnumerable<string> ViewTypes => viewTypes;
        internal static IEnumerable<IViewDef> FindView(List<Func<IViewDef, bool>> filters)
        {
            if (filters is null) { return viewList; }
            IEnumerable<IViewDef> retval = viewList;
            foreach (var f in filters)
            {
                retval = retval.Where(f);
            }
            return retval.ToArray();
        }

        internal static bool Ready() => viewList.Any();

        internal static void GetViewDefinitions()
        {
            viewList.Clear();
            using (var db = new CommenceDatabase()) {
                categoryNames = db.GetCategoryNames();
                foreach (string categoryName in Categories) {
                    foreach (string viewName in db.GetViewNames(categoryName)) {
                        viewList.Add(db.GetViewDefinition(viewName));
                    }
                }
                viewTypes = viewList.Select(s => s.Type).Distinct();
                categoryNames = categoryNames.Distinct();
            }
        }

    }
}