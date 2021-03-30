using System;
using System.Collections.Generic;
using System.Text;

namespace  PoshCommence.Base
{
    [Obsolete]
    public static class EnumHelper
    {
        public static IEnumerable<object> ListEnum<T>() where T: Enum
        {
            foreach (int t in Enum.GetValues(typeof(T)))
            {
                StringBuilder sb = new StringBuilder("[");
                sb.Append(typeof(T).Namespace);
                sb.Append("]::");
                sb.Append(Enum.GetName(typeof(T), t));
                yield return new { Qualifier = sb.ToString(), Value = t };
            }
        }
    }
}