using System;
using Xunit;

namespace tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);         
        }

          [Fact]
        public void Test2()
        {
            Assert.False(false);         
        }

           [Fact]
        public void Test3()
        {
            Assert.Equal(5,5);         
        }
    }
}
