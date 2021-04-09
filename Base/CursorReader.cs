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
        // when the umber of items is large;
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
                    cur.SetColumns(fieldNames.Cast<object>().ToArray());
                    if (relatedColumns != null) 
                    {
                        foreach (var rc in relatedColumns)
                        {
                            cur.SetRelatedColumn(cur.ColumnCount, 
                                rc.ConnectionName,
                                rc.ToCategory,
                                rc.FieldName);
                        }
                    }
                    retval = cur.ReadAllRows();
                }
            }
            return retval;
        }
    } 
}