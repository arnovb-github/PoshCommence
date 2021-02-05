using System;
using System.Collections.Generic;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Export;

namespace PSCommenceModules.Base
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

        internal static void ExportCursor(string categoryName, IEnumerable<ICursorFilter> filters, string[] fieldNames, string path, IExportSettings exportOptions)
        {
            // we got fieldnames, so we have to create a custom cursor
            using (var db = new CommenceDatabase())
            using (var cur = db.GetCursor(categoryName))
            {
                cur.Columns.AddDirectColumns(fieldNames);
                cur.Columns.Apply();
                if (filters != null)
                {
                    foreach (var f in filters)
                    {
                        cur.Filters.Add(f);
                    }
                    cur.Filters.Apply();
                }
                cur.ExportToFile(path, exportOptions);
            }
        }
    }
}