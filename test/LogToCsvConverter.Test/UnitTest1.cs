using System;
using Xunit;

namespace LogToCsvConverter.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var x = 5;
            var y = 3;
            var expected = 8;

            var actual = x + y;

            Assert.Equal(expected, actual);
        }
    }
}
