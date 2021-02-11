namespace PoshCommence.Base
{
    public class CommenceField
    {
        internal CommenceField(string categoryName, 
                        string fieldName, 
                        string fieldValue)
        {
            CategoryName = categoryName;
            FieldName = fieldName;
            FieldValue = fieldValue;
        }
        public string CategoryName { get;} 
        public string FieldName { get; }
        public string FieldValue { get; }
    }
}
