using System;
using System.Collections.Generic;
using System.Linq;

namespace Net.Extensions
{
    public static class DictionaryExtensions
    {
        public static ReadOnlyDictionary<TKey,T> ToReadOnlyDictionary<T,TKey>(this IEnumerable<T> items, Func<T,TKey> fnKey)
        {
            if (items.IsNull()) return new ReadOnlyDictionary<TKey, T>();
            var pairs = items.AsSafeEnumerable()
                .Select(p =>
                {
                    var key = fnKey(p);
                    return new KeyValuePair<TKey, T>(key, p);
                });
            return new ReadOnlyDictionary<TKey, T>(pairs);
        }
        public static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<T, TKey,TValue>(this IEnumerable<T> items, Func<T, TKey> fnKey,Func<T,TValue> fnValue)
        {
            if (items.IsNull()) return new ReadOnlyDictionary<TKey, TValue>();
            var pairs = items.AsSafeEnumerable()
                .Select(p =>
                {
                    var key = fnKey(p);
                    var value = fnValue(p);
                    return new KeyValuePair<TKey, TValue>(key, value);
                });
            return new ReadOnlyDictionary<TKey, TValue>(pairs);
        }
        public static TValue GetSafeValue<TKey,TValue> (this IDictionary<TKey,TValue> dic, TKey key)
        {
            if (key == null || dic==null) return default(TValue);
            return dic.ContainsKey(key) ? dic[key] : default(TValue);
        }

        public static IDictionary<string,object> MergeDictionary(this IDictionary<string,object> target,IDictionary<string,object> source,bool overrideExistings=true)
        {
            target = target ?? new Dictionary<string, object>();
            foreach(var kv in source)
            {
                if (!target.ContainsKey(kv.Key))
                {
                    target.Add(kv.Key, kv.Value);
                    continue;
                }
                var targetValue = target[kv.Key];
                if(!(targetValue is IDictionary<string, object> dicTarget)){
                    if (overrideExistings)
                        target[kv.Key] = kv.Value;
                    continue;
                }
                if (kv.Value is IDictionary<string, object> dicSource)
                    dicTarget.MergeDictionary(dicSource, overrideExistings);
                else if (overrideExistings)
                    target[kv.Key] = kv.Value;
            }
            return target;
        }

        public static IDictionary<string, object> Extend(this IDictionary<string, object> dic, string path, object value, bool overrideExists = true)
        {
            dic = dic ?? new Dictionary<string, object>();
            var paths = path.SplitBy(".");
            var current = dic;
            for (var i = 0; i < paths.Length - 1; i++)
            {
                var p = paths[i];
                if (!current.ContainsKey(p) || !(current[p] is IDictionary<string, object>))
                    current[p] = new Dictionary<string, object>();
                current = current[p] as IDictionary<string, object>;
            }
            var lastPath = paths.Last();
            if (!current.ContainsKey(lastPath))
            {
                current[lastPath] = value;
                return dic;
            }
            var curValue = current[lastPath];
            if (curValue is IDictionary<string, object> dicCur && value is IDictionary<string, object> dicVal)
                dicCur.MergeDictionary(dicVal, overrideExists);
            else if (overrideExists)
                current[lastPath] = value;
            return dic;       
        }
       
    }

  

}
