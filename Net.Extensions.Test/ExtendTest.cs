using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Net.Extensions.Test
{
    public class ExtendTest
    {
        [Fact]
        public void ExtendPropertyTest()
        {
            var x = new Dictionary<string, object>();
            var result = x.Extend("a", "32");
            x.Extend("a.b.e", "oley",false);
        }

        [Fact]
        public void MergeTest()
        {
            var x = new Dictionary<string, object>();
            x.Extend("a.b.e", "oley", false);
            var y = new Dictionary<string, object>();
            y.Extend("a.b.d", "do");
            var result = x.MergeDictionary(y);
        }

    }
}
