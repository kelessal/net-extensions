using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Net.Extensions
{
    public class ConcurrentSet<T>:IEnumerable<T>
    {
        private ConcurrentDictionary<T, byte> items = new ConcurrentDictionary<T, byte>();

        public IEnumerator<T> GetEnumerator() => this.items.Keys.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.items.Keys.GetEnumerator();
        public void Add(T item)
            => this.items[item] = 1;
        public void Remove(T item)
            => this.items.TryRemove(item, out byte result);
        public bool Contains(T item)
            => this.items.ContainsKey(item);

    }
}
