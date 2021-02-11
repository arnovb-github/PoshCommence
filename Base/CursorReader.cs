using System.Collections.Generic;
using System.Linq;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.Base
{
    internal static class CursorReader
    {
        // notice we do not do any error checking
        // we just let the error pass through to Powershell
        // Not sure if that's a good idea
        public static List<List<string>> GetCmcFieldValues(
            string categoryName,
                IEnumerable<string> fieldNames,
                IEnumerable<ICursorFilter> filters,
                IEnumerable<RelatedColumn> relatedColumns,
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