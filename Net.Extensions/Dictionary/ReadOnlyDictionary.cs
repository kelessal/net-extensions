using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net.Extensions
{
    public class ReadOnlyDictionary<TKey, TValue> : IEnumerable<TValue>
    {
        readonly Dictionary<TKey, TValue> Dictionary = new Dictionary<TKey, TValue>();
        public IEnumerable<TKey> Keys => this.Dictionary.Keys;
        public IEnumerator<TValue> GetEnumerator() => this.Dictionary.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        public TValue this[TKey key]=> this.Dictionary.GetSafeValue(key);
        public ReadOnlyDictionary()
        {

        }
        public ReadOnlyDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            collection.AsSafeEnumerable().Foreach(p =>
            {
                this.Add(p);
            });
        }
        void Add(KeyValuePair<TKey,TValue> item)
        {
            if (item.Key == null) return;
            this.Dictionary[item.Key] = item.Value;
        }
        
    }
}
