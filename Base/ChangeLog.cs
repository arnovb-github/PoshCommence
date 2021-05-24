using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PoshCommence.Base
{
    internal class ChangeLog
    {
        public string DatabaseName { get; set; }
        public string CategoryName { get; set; }

        public DateTime Date { get; set; }

        readonly Dictionary<string, IEnumerable<FieldModification>> modifiedRows =
            new Dictionary<string, IEnumerable<FieldModification>>();

        public Dictionary<string, IEnumerable<FieldModification>> ModifiedRows => modifiedRows;

        internal void WriteToFile(string path)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (var tw = new StreamWriter(path))
            {
                tw.Write(json);
            }
        }

    }

    internal class FieldModification
    {
        internal FieldModification(string itemName, string fieldName, string oldValue, string newValue, int modifySuccess)
        {
            ItemName = itemName;
            FieldName = fieldName;
            OldValue = oldValue;
            NewValue = newValue;
            ModifySuccess = modifySuccess;
        }

        public string ItemName { get; }
        public string FieldName { get; }
        public string OldValue { get; }
        public string NewValue { get; }
        public int ModifySuccess { get; }
    }
}