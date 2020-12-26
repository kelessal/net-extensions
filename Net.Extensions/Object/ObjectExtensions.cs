using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Net.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T item)
            where T : class
            => item == null;
        public static bool Intersects<T>(this T item,T start,T end)
            where T:IComparable<T>
        {
            if (start.CompareTo(item) == 1) return false;
            if (end.CompareTo(item) == -1) return false;
            return true;
        }
        public static bool Intersects<T>(this T item, Range<T> range)
            where T :struct, IComparable<T>
        {
            return range.Intersects(item);
        }
        public static IEnumerable<T> SingleToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static Array AsArray(this object item)
        {
            if (item == null) return Array.Empty<object>();
            if (item is Array) return item as Array;
            return new[] { item };
        }
        
    }
}
