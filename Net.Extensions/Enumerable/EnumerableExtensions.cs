using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Net.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Foreach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items.IsEmpty()) return;
            foreach (var item in items)
                action(item);
        }

        public static void Foreach<T>(this IEnumerable<T> items, Action<T, int> action)
        {
            int i = 0;
            foreach (var item in items)
                action(item, i++);
        }
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            if (items == null) return true;
            return !items.Any();
        }
        public static bool IsEmpty(this IEnumerable items)
        {
            if (items == null) return true;
            foreach (var i in items)
                return false;
            return true;
        }
        public static bool IsEmpty<T>(this T[] items)
        {
            if (items == null) return true;
            return !items.Any();
        }
        public static T SafeFirstOrDefault<T>(this IEnumerable<T> items,Func<T,bool> predicate=default)
        {
            if (items == null) return default;
            if (predicate == null) return items.FirstOrDefault();
            return items.FirstOrDefault(predicate);
        }
        public static bool SafeContains<T>(this T[] items, T item)
        {
            if (items.IsEmpty()) return false;
            return items.Contains(item);
        }
        public static bool SafeContains<T>(this HashSet<T> items, T item)
        {
            if (items.IsEmpty()) return false;
            return items.Contains(item);
        }
        public static bool SafeAny<T>(this T[] items, Func<T, bool> exp)
        {
            if (items.IsEmpty()) return false;
            return items.Any(exp);
        }
        public static HashSet<T> ToSafeHashSet<T>(this IEnumerable<T> items)
            => items == null ? new HashSet<T>() : new HashSet<T>(items);
        public static Tree<T> ToTree<T>(this IEnumerable<T> items, Func<T, string> idFn, Func<T, string> parentFn)
        {
            return new Tree<T>(idFn, parentFn, items);
        }
        public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> range)
        {
            foreach (var item in range)
                set.Add(item);
        }
        public static void AddIfNotExists<TKey, TValue>(this IDictionary<TKey, TValue> set, IEnumerable<TValue> range, Func<TValue, TKey> keyFn)
        {
            if (range.IsEmpty()) return;
            foreach (var item in range)
            {
                var key = keyFn(item);
                if (set.ContainsKey(key)) continue;
                set.Add(key, item);
            }
        }
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> set, IEnumerable<TValue> range, Func<TValue, TKey> keyFn)
        {
            if (range.IsEmpty()) return;
            foreach (var item in range)
            {
                var key = keyFn(item);
                set[key] = item;
            }
        }

        public static string ToJoinedString(this IEnumerable<string> strs, string seperator = "")
        {
            if (strs.IsEmpty()) return String.Empty;
            return strs.Aggregate((pre, next) => $"{pre}{seperator}{next}");
        }

        public static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> items)
        {
            return items.Aggregate((pre, next) => pre.Intersect(next));
        }
        public static IEnumerable<T> AsSafeOfType<T>(this object enumerableItem)
        {
            if (enumerableItem == null) yield break;
            if(enumerableItem is IEnumerable  list)
            {

                foreach (T item in list.OfType<T>())
                    yield return item;
            }
        }
        public static IEnumerable<T> AsSafeEnumerable<T>(this IEnumerable<T> list)
        {
            if (list == null) yield break;
            foreach (var i in list)
                yield return i;
        }
    }
}
