using System.Collections.Generic;
using System.Linq;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.Base
{
    internal static class CursorReader
    {
        // notice we do not do any error checking
        // we just let the error pass through to Powershell
        // This means the end user (me!) can see pretty cryptic error messages
        // but I don't care for now.
        
        // TODO: we should incorporate some kind of warning mechanism
        // when the number of items is large;
        // in that case the read may take a long time
        // It would require a significant overhaul of this class though.
        public static List<List<string>> GetCmcFieldValues(
            string categoryName,
                IEnumerable<string> fieldNames,
                IEnumerable<ICursorFilter> filters,
                IEnumerable<ConnectedField> relatedColumns,
                bool useView,
                bool useThids)
        {
            var retval = new List<List<string>>();
            using (var db = new CommenceDatabase())
            {
                using (var cur = db.GetCursor(categoryName,
                    useView ? CmcCursorType.View : CmcCursorType.Category,
                    useThids ? CmcOptionFlags.UseThids : CmcOptionFlags.Default))
                {
                    if (filters != null)
                    {
                        foreach (var f in filters)
                        {
                            cur.Filters.Add(f);
                        }
                        cur.Filters.Apply();
                    }

                    if (fieldNames != null)
                    {
                        // by default all columns are included in a cursor
                        // the GetColumns method takes care of setting the column index
                        cur.SetColumns(fieldNames.Cast<object>().ToArray());
                    }

                    // for related columns, we need a manual way to make sure we get the correct columnnumber
                    int columnIndex = cur.ColumnCount; // we want to start after last position by default
                    if (relatedColumns != null)
                    {
                        // make sure we start at 0 if there were no fields provided
                        // we do that because when a cursor is created, by default all columns are included
                        if (fieldNames == null) { columnIndex = 0; } 
                        foreach (var rc in relatedColumns)
                        {
                            cur.SetRelatedColumn(columnIndex, 
                                rc.ConnectionName,
                                rc.ToCategory,
                                rc.FieldName);
                        }
                        columnIndex = cur.ColumnCount;
                    }
                    retval = cur.ReadAllRows(); // this may be slow! No CancellationToken possible
                }
            }
            return retval;
        }
    } 
}