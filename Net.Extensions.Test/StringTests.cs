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
        [Fact]
        public void CamelCaseTest()
        {
            var x = "ABDServiceSo.DEFS.GH";
            var result = x.ToCamelCase();

        }
        [Fact]
        public void IsCaseInsensitiveEndsWithTest()
        {
            Assert.True("HelloWorld".IsCaseInsensitiveEndsWith("world"));
            Assert.False("HelloWorld".IsCaseInsensitiveEndsWith("Hello"));
            Assert.False("HelloWorld".IsCaseInsensitiveEndsWith("HelloWorld."));
        }
        [Fact]
        public void ToShortIdTest()
        {
            var x = Guid.NewGuid();
            var id=x.ToShortId();
        }
        [Fact]
        public void AsArrayTest()
        {
            var x =  "hello world";
            var y = new[] { x };
            var result=y.AsArray();
        }
    }
}
