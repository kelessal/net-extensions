using System;
using Xunit;

namespace Net.Extensions.Test
{
    public class RangeTests
    {
        [Theory]
        [InlineData(0,0,0,0)]
        public void EqualTest(int value1,int value2,int value3,int value4)
        {
            var range1 = new Range<int>(value1, value2);
            var range2 = new Range<int>(value3, value4);
            Assert.Equal(range1, range2);
        }
        [Theory]
        [InlineData(0, 0, 0, 1)]
        public void NotEqualTest(int value1, int value2, int value3, int value4)
        {
            var range1 = new Range<int>(value1, value2);
            var range2 = new Range<int>(value3, value4);
            Assert.NotEqual(range1, range2);
        }
        [Theory]
        [InlineData(0, 0,-1, 2)]
        public void IntersectTest(int value1, int value2, int value3, int value4)
        {
            var range1 = new Range<int>(value1, value2);
            var range2 = new Range<int>(value3, value4);
            Assert.True(range1.Intersects(range2));
        }
        [Theory]
        [InlineData(0, 0, 1, 2)]
        public void NotIntersectTest(int value1, int value2, int value3, int value4)
        {
            var range1 = new Range<int>(value1, value2);
            var range2 = new Range<int>(value3, value4);
            Assert.False(range1.Intersects(range2));
        }
    }
}
