using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;

namespace WebAPI_UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestSuccess1()
        {
        }

        [Fact]
        public void TestSuccess2()
        {
        }

        [Theory]
        [InlineData("https://www.google.com")]
        [InlineData("fffaaa")]
        public void TestMethod1(string uri)
        {
            new Uri(uri);
        }
    }
}
