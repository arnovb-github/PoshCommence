using System;
using System.Collections.Generic;

namespace PoshCommence.Base
{
    internal class ChangeLog
    {
        internal string DatabaseName { get; set; }
        internal string CategoryName { get; set; }

        internal DateTime Date { get; set; }

        readonly Dictionary<string, IEnumerable<FieldModification>> modifiedRows =
            new Dictionary<string, IEnumerable<FieldModification>>();

        internal Dictionary<string, IEnumerable<FieldModification>> ModifiedRows => modifiedRows;
    }

    internal class FieldModification
    {
        internal FieldModification(string itemName, string fieldName, string oldValue, string newValue)
        {
            ItemName = itemName;
            FieldName = fieldName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string ItemName { get; }
        internal string FieldName { get; }
        internal string OldValue { get; }
        internal string NewValue { get; }
    }
}