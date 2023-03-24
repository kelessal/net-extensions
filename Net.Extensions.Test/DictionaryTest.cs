using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Net.Extensions.Test
{
    public class DictionaryTest
    {
        [Fact]
        public void ReadOnlyDicTest()
        {
            var x = new[] { new { Id = 1,Value="11" }, new { Id = 2,Value="22" } };
            var dic=x.ToReadOnlyDictionary(p => p.Id,p=>p.Value);
            var result=dic[2];
        }

        [Fact]
        public void TreeTest()
        {
            var items = new[] { new TreeItem { Id = "1", Parent = "3" }, new TreeItem { Id = "2", Parent = "1" }, new TreeItem { Id = "3" } };
            var tree=   items.ToTree(p=>p.Id,p=>p.Parent);
            tree.Add(new TreeItem { Id = "4", Parent = "3" });
            var descends=tree["4"].GetAscendings(default,true).Select(p=>p.Data).ToArray();
        }
    }
}
