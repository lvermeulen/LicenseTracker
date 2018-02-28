using System;
using System.Threading.Tasks;
using Flurl.Http;
using Licenses.Core;
using Xunit;

namespace Licenses.Sources.GitHub.Tests
{
    public class GitHubLicenseSourceShould
    {
        private readonly ILicenseSource _source = new GitHubLicenseSource();

        public GitHubLicenseSourceShould()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new GitHubLicenseSourceHttpClientFactory());
        }

        [Fact]
        public async Task GetLicensesAsync()
        {
            var results = await _source.GetLicensesAsync();
            Assert.Contains(results, x => x.Key.Equals("MIT", StringComparison.OrdinalIgnoreCase));
        }
    }
}
