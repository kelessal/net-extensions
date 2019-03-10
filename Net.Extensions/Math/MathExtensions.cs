using System;
using System.Collections.Generic;
using System.Linq;

namespace Net.Extensions
{
    public static class MathExtensions
    {
        public static IEnumerable<IEnumerable<T>> Combine<T>(this IEnumerable<IEnumerable<T>> items)
        {
            var fullList = items.ToArray();
            foreach (var list in Combine(fullList, 0, new T[] { }).ToArray())
                yield return list;
        }
        private static IEnumerable<IEnumerable<T>> Combine<T>(IEnumerable<T>[] fullList, int currentIndex, IEnumerable<T> parentItems)
        {
            if (currentIndex >= fullList.Length )
            {
                yield return parentItems;
                yield break;
            }
            var current = fullList[currentIndex];
            foreach (var item in current)
            {
                var withMyItem = parentItems.Concat(item.SingleToEnumerable());
                foreach (var subItem in Combine(fullList, currentIndex + 1, withMyItem))
                    yield return subItem;
            }
        }

        public static IEnumerable<Tuple<T,T>> GetBinaryCombinations<T>(this IEnumerable<T> items)
        {

            var liste = items.ToArray();
            for (var i = 0; i < liste.Length; i++)
                for (var j = i + 1; j < liste.Length; j++)
                    yield return new Tuple<T, T>(liste[i], liste[j]);
        }



    }
}
