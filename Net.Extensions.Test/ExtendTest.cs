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
            var result = x.Extend("a.b.c", "32");
            x.Extend("a.b.e", "oley",false);
        }

    }
}
