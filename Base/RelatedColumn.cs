/* Notice that the namespace is PoshCommence and not PoshCommence.Base
    This is because it would be confusing to create the type in Powershell
    New-Object -TypeName poshcommence.base.relatedcolumn <- not intuitive
*/
namespace PoshCommence
{
    public class RelatedColumn
    {
        string delim = "%%";
        public RelatedColumn() {}
        public RelatedColumn(string c, string t, string f)
        {
            ConnectionName = c;
            ToCategory = t;
            FieldName = f;
        }
        public string ConnectionName { get; set; }
        public string ToCategory { get; set; }
        public string FieldName { get; set; }

        // create a columnname
        internal string ColumnName => '%'+ ConnectionName + delim  + ToCategory + delim + FieldName + '%';

    }
}