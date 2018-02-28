using System;
using Xunit;

namespace NuGet.Net.Tests
{
    public class StringExtensionsShould
    {
        [Fact]
        public void ThrowArgumentNullExceptionWhenUrlIsNull()
        {
            Assert.Throws<ArgumentNullException>("url", () => StringExtensions.ForNuspec(null, ""));
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenPackageNameIsNull()
        {
            Assert.Throws<ArgumentNullException>("packageName", () => "".ForNuspec(null));
        }
    }
}
