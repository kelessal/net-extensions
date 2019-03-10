using System.Collections.Generic;
namespace Net.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetSafeValue<TKey,TValue> (this IDictionary<TKey,TValue> dic, TKey key)
        {
            if (key == null || dic==null) return default(TValue);
            return dic.ContainsKey(key) ? dic[key] : default(TValue);
        }
       
    }
}
