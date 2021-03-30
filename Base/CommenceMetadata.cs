using System;
using System.Collections.Generic;
using System.Linq;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;

namespace PoshCommence.Base
{
    // this class acts as a sort of poor man's cache for autocompletion
    internal static class CommenceMetadata
    {
        private static IEnumerable<string> categoryNames = new List<string>();
        private static IEnumerable<string> viewNames = new List<string>();
        private static List<IViewDef> viewList = new List<IViewDef>();
        private static IEnumerable<string> viewTypes;
        private static IDictionary<string, IEnumerable<string>> fieldNames = new Dictionary<string, IEnumerable<string>>();
        private static IDictionary<string, IEnumerable<ICommenceConnection>> connections = new Dictionary<string, IEnumerable<ICommenceConnection>>();

        static CommenceMetadata()
        {
            // git has no idea about this. CommenceProcessMonitor lives in a separate shared project.
            var monitor = new CommenceProcessMonitor.ProcessMonitor(); 
            monitor.CommenceDatabaseChanged += Monitor_CommenceDatabaseChanged;
        }

        private static void Monitor_CommenceDatabaseChanged(object sender, CommenceProcessMonitor.CommenceDatabaseChangedArgs e)
        {
            ClearAll();
        }

        #region Categories
        internal static IEnumerable<string> Categories
        {
            get
            {
                if (!categoryNames.Any())
                {
                    RefreshCategories();
                }
                return categoryNames;
            }

        }

        private static void RefreshCategories()
        {
            using (var db = new CommenceDatabase())
            {
                categoryNames = db.GetCategoryNames();
            }
        }
        #endregion

        #region Views
        internal static IEnumerable<string> Views
        {
            get
            {
                if (!viewNames.Any())
                {
                    RefreshViewNames();
                }
                return viewNames;
            }
        }

        private static void RefreshViewNames()
        {
            // gets all viewnames
            using (var db = new CommenceDatabase())
            {
                foreach (string categoryName in Categories)
                {
                    viewNames = viewNames.Concat(db.GetViewNames(categoryName));
                }
            }
        }

        internal static IEnumerable<IViewDef> ViewDefinitions
        {
            get
            {
                if ((!viewList.Any()))
                {
                    RefreshViewDefinitions();
                }
                return viewList;
            }
        }
        internal static IEnumerable<string> ViewTypes => viewTypes;
        internal static IEnumerable<IViewDef> FindView(List<Func<IViewDef, bool>> filters)
        {
            if (filters is null) { return ViewDefinitions; }
            IEnumerable<IViewDef> retval = ViewDefinitions;
            foreach (var f in filters)
            {
                retval = retval.Where(f);
            }
            return retval.ToArray();
        }

        internal static void RefreshViewDefinitions()
        {
            viewList.Clear();
            using (var db = new CommenceDatabase())
            {
                foreach (string viewName in Views)
                {
                    viewList.Add(db.GetViewDefinition(viewName));
                }
            }
            viewTypes = viewList.Select(s => s.Type).Distinct();
        }
        #endregion

        #region Fields
        internal static IEnumerable<string> FieldNames(string categoryName)
        {
            if (!fieldNames.Any() || (Categories.Contains(categoryName) && !fieldNames.ContainsKey(categoryName)))
            {
                AddFieldNamesForCategory(categoryName);
            }

            IEnumerable<string> retval;
            fieldNames.TryGetValue(categoryName, out retval);
            return retval;
        }

        private static void AddFieldNamesForCategory(string categoryName)
        {
            using (var db = new CommenceDatabase())
            {
                fieldNames.Add(categoryName, db.GetFieldNames(categoryName));
            }
        }
        #endregion

        #region Connections
        internal static IDictionary<string, IEnumerable<ICommenceConnection>> Connections
        {
            get
            {
                if (!connections.Any())
                {
                    RefreshConnections();
                }
                return connections;
            }
        }

        private static void RefreshConnections()
        {
            using (var db = new CommenceDatabase())
            {
                foreach (string c in Categories)
                {
                    connections.Add(c, db.GetConnectionNames(c));
                }
            }
        }
        #endregion

        internal static void ClearAll()
        {
            categoryNames = new List<string>();
            fieldNames = new Dictionary<string, IEnumerable<string>>();
            viewNames = new List<string>();
            viewList = new List<IViewDef>();
            connections = new Dictionary<string, IEnumerable<ICommenceConnection>>();
        }
      
    }
 }