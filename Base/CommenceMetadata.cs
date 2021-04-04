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
        private static IDictionary<CommenceFieldType, IEnumerable<FilterQualifier>> validFieldQualifiers;

        static CommenceMetadata()
        {
            // https://github.com/arnovb-github/CommenceProcessMonitor
            var monitor = new CommenceProcessMonitor.ProcessMonitor(); 
            monitor.CommenceDatabaseChanged += Monitor_CommenceDatabaseChanged;
            validFieldQualifiers = CreateValidFieldQualifiers();
        }

        private static void Monitor_CommenceDatabaseChanged(object sender, CommenceProcessMonitor.CommenceDatabaseChangedArgs e)
        {
            ClearAll();
        }

        /*
            It would be nice to be able to check if the qualifiers used in a filter
            are actually applicable to the specfied fieldname(s).
            There is a problem with that:
            A filter has no knowledge of the underlying cateory.
            I suppose I could add that, but it is extra typing for the end-user
            and he would not see the category name in the resulting DDE ViewFilter string.
            On the other hand, if we are taking the object oriented approach to filtering,
            it would kind of make sense to have to provide the underling category as well.
            I am currently in two minds about this.
        */
        private static IDictionary<CommenceFieldType, IEnumerable<FilterQualifier>> CreateValidFieldQualifiers()
        {
            // this just populates a static list
            var retval = new Dictionary<CommenceFieldType, IEnumerable<FilterQualifier>>();
            retval.Add(CommenceFieldType.Name, new List<FilterQualifier>() {
                FilterQualifier.Between,
                FilterQualifier.Blank,
                FilterQualifier.Contains,
                FilterQualifier.DoesNotContain,
                FilterQualifier.EqualTo,
                // notice the absence of HasDuplicates
            });
            retval.Add(CommenceFieldType.Calculation, new List<FilterQualifier>() {
                FilterQualifier.Between,
                FilterQualifier.EqualTo,
                FilterQualifier.GreaterThan,
                FilterQualifier.LessThan,
                FilterQualifier.NotEqualTo
            });
            retval.Add(CommenceFieldType.Checkbox, new List<FilterQualifier>() {
                FilterQualifier.Checked,
                FilterQualifier.False,
                FilterQualifier.No,
                FilterQualifier.NotChecked,
                FilterQualifier.One,
                FilterQualifier.True,
                FilterQualifier.Yes,
                FilterQualifier.Zero
            });
            retval.Add(CommenceFieldType.Datafile, new List<FilterQualifier>()); // Data File fields cannot be filtered
            retval.Add(CommenceFieldType.Date, new List<FilterQualifier>() {
                FilterQualifier.After,
                FilterQualifier.Before,
                FilterQualifier.Between,
                FilterQualifier.Blank,
                FilterQualifier.On
            });
            retval.Add(CommenceFieldType.Email, new List<FilterQualifier>() {
                FilterQualifier.After,
                FilterQualifier.Before,
                FilterQualifier.Between,
                FilterQualifier.Blank,
                FilterQualifier.On
            });
            retval.Add(CommenceFieldType.ExcelCell, new List<FilterQualifier>());
            retval.Add(CommenceFieldType.Image, new List<FilterQualifier>());                                      
            retval.Add(CommenceFieldType.Number, new List<FilterQualifier>() {
                FilterQualifier.Between,
                FilterQualifier.EqualTo,
                FilterQualifier.GreaterThan,
                FilterQualifier.LessThan,
                FilterQualifier.NotEqualTo
            });
            retval.Add(CommenceFieldType.Selection, new List<FilterQualifier>() {
                FilterQualifier.EqualTo,
                FilterQualifier.NotEqualTo
            });
            retval.Add(CommenceFieldType.Sequence, new List<FilterQualifier>() {
                FilterQualifier.Between,
                FilterQualifier.EqualTo,
                FilterQualifier.GreaterThan,
                FilterQualifier.LessThan,
                FilterQualifier.NotEqualTo
            });
            retval.Add(CommenceFieldType.Telephone, new List<FilterQualifier>() {
                FilterQualifier.Blank,
                FilterQualifier.Contains,
                FilterQualifier.DoesNotContain,
                FilterQualifier.EqualTo
            });
            retval.Add(CommenceFieldType.Text, new List<FilterQualifier>() {
                FilterQualifier.Between,
                FilterQualifier.Blank,
                FilterQualifier.Contains,
                FilterQualifier.DoesNotContain,
                FilterQualifier.EqualTo,
            });
            retval.Add(CommenceFieldType.Time, new List<FilterQualifier>() {
                FilterQualifier.After,
                FilterQualifier.After,
                FilterQualifier.Before,
                FilterQualifier.Between,
                FilterQualifier.Blank,
            });                           
            retval.Add(CommenceFieldType.URL, new List<FilterQualifier>() {
                FilterQualifier.Contains,
                FilterQualifier.DoesNotContain,
                FilterQualifier.Between,
                FilterQualifier.Blank
            });            
            return retval;
        }


        internal static bool IsQualifierValidForField(ICursorFilter filter)
        {
            switch (filter.GetType())
            {
                case ICursorFilterTypeF f:
                    return false;
                case ICursorFilterTypeCTCF ctcf:
                    return false;
            }
            return false;
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