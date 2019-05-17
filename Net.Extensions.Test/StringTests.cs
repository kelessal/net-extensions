using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Net.Extensions.Test
{
    public class StringTests
    {
        [Fact]
        public void ExtendPropertyTest()
        {
            var x = "HelloWorld";
            var result = x.ToWordsCase();
            Assert.Equal("Hello World", result);
        }
        [Fact]
        public void DashCaseTest()
        {
            var x = "ServiceSo";
            var result = x.ToDashCase();
            
        }
    }
}
