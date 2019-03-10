using System.Collections.Generic;

namespace Net.Extensions
{
    public interface INode<T>
        where T:INode<T>
    {
        IEnumerable<T> Children { get; set; }
    }
}
