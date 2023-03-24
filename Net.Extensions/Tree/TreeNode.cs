using System;
using System.Collections.Generic;
using System.Linq;

namespace Net.Extensions
{
    public sealed class TreeNode<T>
    {
        public T Data { get; private set; }
        public TreeNode<T> Parent { get; private set; }
        private List<TreeNode<T>> _Children = new List<TreeNode<T>>();
        public IEnumerable<TreeNode<T>> Children => this._Children.AsEnumerable();
        internal TreeNode(T model)
        {
            this.Data = model;
        }
        internal void AddChild(TreeNode<T> child)
        {
            this._Children.Add(child);
            child.Parent = this;
        }

        public IEnumerable<TreeNode<T>> Filter(Func<TreeNode<T>, bool> filterFn)
        {
            if (!filterFn(this)) yield break;
            yield return this;
            foreach (var child in this.Children)
                foreach (var subitem in child.Filter(filterFn))
                    yield return subitem;
        }
        public TMap MapTo<TMap>(Func<T,TMap> mapFunction)
            where TMap:INode<TMap>
        {
            var result = mapFunction(this.Data);
            result.Children = this.Children.Select(p => p.MapTo(mapFunction)).ToArray();
            return result;
        }

        public void Sort<TField>(Func<TreeNode<T>,TField> sortFn)
        {
            this._Children = this._Children.OrderBy(sortFn).ToList();
            foreach (var item in this._Children)
                item.Sort(sortFn);
        }
      
        public IEnumerable<TreeNode<T>> GetDescendings(Func<TreeNode<T>,bool> predicate=null,bool includeSelf=true)
        {
            if (includeSelf && (predicate == null || predicate(this))) yield return this;
            foreach (var item in this.Children)
                foreach (var subitem in item.GetDescendings(predicate))
                    yield return subitem;
            
        }
        
        public TreeNode<T> RootNode
        {
            get
            {
                var current = this;
                while (current.Parent != null)
                    current = current.Parent;
                return current;
            }
        }
        public IEnumerable<TreeNode<T>> GetAscendings(Func<TreeNode<T>, bool> predicate=null,bool includeSelf=true)
        {
            if (includeSelf && (predicate==null ||  predicate(this))) yield return this;
            if (this.Parent != null)
                foreach (var parent in this.Parent.GetAscendings(predicate))
                    yield return parent;
        }
    }
}
