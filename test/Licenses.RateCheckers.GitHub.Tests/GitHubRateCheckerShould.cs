using System.Threading.Tasks;
using Xunit;

namespace Licenses.RateCheckers.GitHub.Tests
{
    public class GitHubRateCheckerShould
    {
        private readonly GitHubRateChecker _rateChecker = new GitHubRateChecker();

        [Fact]
        public async Task GetRateLimitsAsync()
        {
            var result = await _rateChecker.GetRateLimitsAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CheckRateLimitsAsync()
        {
            bool result = await _rateChecker.CheckRateLimitsAsync();
            Assert.True(result);
        }

        [Fact]
        public async Task WithRateLimitsActionAsync()
        {
            string text = "Hello";
            await _rateChecker.WithRateLimitsAsync(text, s => text = text + s);
            Assert.Equal("HelloHello", text);
        }

        [Fact]
        public async Task WithRateLimitsFuncAsync()
        {
            string text = "4";
            text = await _rateChecker.WithRateLimitsAsync(int.Parse(text), i => Task.FromResult((++i).ToString()));
            Assert.Equal("5", text);
        }
    }
}
