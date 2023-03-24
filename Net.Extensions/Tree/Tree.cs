using System;
using System.Collections.Generic;
using System.Linq;

namespace Net.Extensions
{
    public sealed class Tree<T>
    {
        readonly Dictionary<string, TreeNode<T>> _itemsDic = new Dictionary<string, TreeNode<T>>();
        List<TreeNode<T>> _children = new List<TreeNode<T>>();
        public IEnumerable<TreeNode<T>> Children => this._children.AsEnumerable();
        readonly Func<T, string> _ParentIdFn;
        readonly Func<T, string> _IdFn;

        public IEnumerable<T> GetItems()
        {
            return this._itemsDic.Values.Select(p => p.Data);
        }
        internal Tree(Func<T, string> idFn, Func<T, string> parentIdFn, IEnumerable<T> items)
        {
            this._ParentIdFn = parentIdFn ?? throw new ArgumentNullException(nameof(parentIdFn));
            this._IdFn = idFn ?? throw new ArgumentNullException(nameof(idFn));
            this.Init(items);

        }
        public IEnumerable<TMap> MapTo<TMap>(Func<T, TMap> mapFn)
            where TMap : INode<TMap>
        {
            return this.Children.Select(p => p.MapTo(mapFn));
        }
        public TreeNode<T> this[string key]
        {
            get
            {
                if (!this._itemsDic.ContainsKey(key)) return default;
                return this._itemsDic[key];
            }
        }
        void Init(IEnumerable<T> items)
        {
            this._itemsDic.Clear();
            items.Foreach(p =>
            {
                var node = new TreeNode<T>(p);
                _itemsDic.Add(this._IdFn(p), node);
            });
            foreach (var item in this._itemsDic.Values)
            {
                if (this.HasLoop(item.Data)) continue;
                var parentId = this._ParentIdFn(item.Data);
                if (parentId == null || !this._itemsDic.ContainsKey(parentId)) continue;
                var parent = this._itemsDic[parentId];
                parent.AddChild(item);
            }
            this._children = this._itemsDic.Values.Where(p => p.Parent == null).ToList();

        }
        public bool HasLoop(T item)
        {
            var parentId = this._ParentIdFn(item);
            if (parentId == null) return false;
            var id = this._IdFn(item);
            var followedPath = new HashSet<string>();
            followedPath.Add(id);
            var current = item;
            while (parentId != null)
            {
                current = _itemsDic.ContainsKey(parentId) ? this._itemsDic[parentId].Data : default(T);
                if (current == null) return false;
                id = this._IdFn(current);
                if (followedPath.Contains(id)) return true;
                followedPath.Add(id);
                parentId = this._ParentIdFn(current);
            }
            return false;
        }
        public Tree<T> Filter(Func<TreeNode<T>, bool> filterFn)
        {
            var filteredItems = this.Children.SelectMany(p => p.Filter(filterFn))
                .Select(p => p.Data);
            return new Tree<T>(this._IdFn, this._ParentIdFn, filteredItems);
        }

        public IEnumerable<TreeNode<T>> GetDescendings(Func<TreeNode<T>, bool> predicate)
        {
            return this.Children.SelectMany(p => p.GetDescendings(predicate));
        }
        public void Sort<TField>(Func<TreeNode<T>, TField> sortFn)
        {
            this._children = this._children.OrderBy(sortFn).ToList();
            foreach (var item in this._children)
                item.Sort(sortFn);
        }
        public void Add(T item)
        {
            var newItems = this.GetItems().Append(item).Distinct().ToArray();
            this.Init(newItems);
        }
    }
}
