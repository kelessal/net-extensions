using System.Collections.Generic;
using System.Linq;

namespace Net.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetSafeValue<TKey,TValue> (this IDictionary<TKey,TValue> dic, TKey key)
        {
            if (key == null || dic==null) return default(TValue);
            return dic.ContainsKey(key) ? dic[key] : default(TValue);
        }
        public static Dictionary<string, object> Extend(this Dictionary<string, object> dic, string path, object value, bool overrideExists = true)
        {
            dic = dic ?? new Dictionary<string, object>();
            var paths = path.SplitBy(".");
            var current = dic;
            for (var i = 0; i < paths.Length - 1; i++)
            {
                var p = paths[i];
                if (!current.ContainsKey(p) || !(current[p] is Dictionary<string, object>))
                    current[p] = new Dictionary<string, object>();
                current = current[p] as Dictionary<string, object>;
            }
            if (!overrideExists && current.ContainsKey(paths.Last())) return dic;
            current[paths.Last()] = value;
            return dic;
        }
       
    }
}
