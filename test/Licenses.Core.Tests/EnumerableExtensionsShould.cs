using System.Collections.Generic;
using Xunit;

namespace Licenses.Core.Tests
{
    public class EnumerableExtensionsShould
    {
        private IEnumerable<string> IgnoreBetween(IEnumerable<string> items)
        {
            return items.IgnoreBetween("beginOptional", "endOptional");
        }

        [Fact]
        public void IgnoreBetweenNoneFound()
        {
            var items = new List<string>
            {
                "one",
                "two",
                "three"
            };
            var results = IgnoreBetween(items);
            Assert.Equal(items, results);
        }

        [Fact]
        public void IgnoreBetweenFirstFound()
        {
            var items = new List<string>
            {
                "one",
                "twobeginoptionalthree",
                "four",
                "five",
                "eight"
            };
            var results = IgnoreBetween(items);
            var expected = new List<string> { "one" };
            Assert.Equal(expected, results);
        }

        [Fact]
        public void IgnoreBetweenLastFound()
        {
            var items = new List<string>
            {
                "one",
                "four",
                "five",
                "sixendoptionalseven",
                "eight"
            };
            var results = IgnoreBetween(items);
            Assert.Equal(items, results);
        }

        [Fact]
        public void IgnoreBetweenFirstAndLastFound()
        {
            var items = new List<string>
            {
                "one",
                "twobeginoptionalthree",
                "four",
                "five",
                "sixendoptionalseven",
                "eight"
            };
            var results = IgnoreBetween(items);
            var expected = new List<string> { "one", "eight" };
            Assert.Equal(expected, results);
        }
    }
}
