using System.Collections.Generic;
using System.Linq;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Export;

namespace PoshCommence.Base
{
    internal static class CommenceExporter
    {
        internal static void ExportCategory(string name, IEnumerable<ICursorFilter> filters, string path, IExportSettings options)
        {

            ExportEngine ee = new ExportEngine();
            if (filters == null)
            {
                ee.ExportCategory(name, path, options);
            }
            else
            {
                ee.ExportCategory(name, filters, path, options);
            }
            ee.Close(); // does nothing, not needed
        }

        internal static void ExportCursor(string categoryName, IEnumerable<ICursorFilter> filters, string[] fieldNames, IEnumerable<ConnectedField> connectedFields, string path, IExportSettings options)
        {
            // we got fieldnames, so we have to create a custom cursor
            using (var db = new CommenceDatabase())
            using (var cur = db.GetCursor(categoryName))
            {
                if (fieldNames != null && fieldNames.Any())
                {
                    cur.Columns.AddDirectColumns(fieldNames);
                }
                // in this case the cursor will hold all columns, because non was exlicitly specified
                // we need to explicitly clear it
                // in this case we will do so by adding the Name field
                else
                {
                    cur.Columns.AddDirectColumn(db.GetNameField(categoryName));
                }
                
                if (connectedFields != null)
                {
                    foreach (var cf in connectedFields)
                    {
                        cur.Columns.AddRelatedColumn(cf.ConnectionName, cf.ToCategory, cf.FieldName);
                    }
                }
                cur.Columns.Apply();
                
                if (filters != null)
                {
                    foreach (var f in filters)
                    {
                        cur.Filters.Add(f);
                    }
                    cur.Filters.Apply();
                }
                cur.ExportToFile(path, options);
            }
        }

        internal static void ExportView(string viewName, string path, IExportSettings options)
        {
            ExportEngine ee = new ExportEngine();
            ee.ExportView(viewName, path, options);
            ee.Close(); // does nothing, not needed
        }
    }
}