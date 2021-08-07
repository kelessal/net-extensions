using System;
using System.Collections.Generic;
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
    }
}
