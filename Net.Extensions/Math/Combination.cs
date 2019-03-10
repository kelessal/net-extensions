using System.Collections;
using System.Collections.Generic;

namespace Net.Extensions
{
    public sealed class Combination<T> : IEnumerable<T>
    {
        List<T> items = new List<T>();

        internal void AddItem(T item)
        {
            this.items.Add(item);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
