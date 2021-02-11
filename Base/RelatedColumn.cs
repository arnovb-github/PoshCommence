namespace PoshCommence.Base
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