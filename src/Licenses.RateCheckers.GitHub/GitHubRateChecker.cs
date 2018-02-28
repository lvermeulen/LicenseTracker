using System;
using System.Threading.Tasks;
using Flurl.Http;
using Licenses.Core;
using Licenses.RateCheckers.GitHub.Models;

namespace Licenses.RateCheckers.GitHub
{
    public class GitHubRateChecker : ICheckRateLimits
    {
        public Task<RateLimits> GetRateLimitsAsync()
        {
            return "https://api.github.com/rate_limit"
                .WithHeader("User-Agent", "LicenseTracker")
                .GetJsonAsync<RateLimits>();
        }

        public async Task<bool> CheckRateLimitsAsync()
        {
            var rateLimits = await GetRateLimitsAsync();

            return rateLimits
                .Resources
                .Core
                .Remaining > 0;
        }

        public async Task WithRateLimitsAsync<T>(T t, Action<T> action)
        {
            if (!await CheckRateLimitsAsync())
            {
                throw new InvalidOperationException("Rate limit exceeded");
            }

            action(t);
        }

        public async Task<TResult> WithRateLimitsAsync<T, TResult>(T t, Func<T, Task<TResult>> selector)
        {
            if (!await CheckRateLimitsAsync())
            {
                throw new InvalidOperationException("Rate limit exceeded");
            }

            return await selector(t);
        }
    }
}
