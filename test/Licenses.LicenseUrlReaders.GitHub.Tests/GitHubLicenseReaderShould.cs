using System.Threading.Tasks;
using Flurl.Http;
using Licenses.Core;
using Licenses.LicenseUrlReaders.Github;
using Xunit;

namespace Licenses.LicenseUrlReaders.GitHub.Tests
{
    public class GitHubLicenseReaderShould
    {
        private readonly ILicenseUrlReader _reader = new GitHubLicenseUrlReader();

        public GitHubLicenseReaderShould()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new GitHubLicenseReaderHttpClientFactory());
        }

        [Fact]
        public async Task GetLicenseAsync()
        {
            string result = await _reader.GetLicenseTextAsync("https://github.com/lvermeulen/Flurl.Http.Xml/blob/master/LICENSE");
            Assert.True(new DefaultLicenseComparer().LicensesEqual("some body", result));
        }
    }
}
