using System.Threading.Tasks;
using Licenses.Core;
using Xunit;

namespace Licenses.LicenseUrlProviders.NuGet.Tests
{
    public class NuGetLicenseHolderShould
    {
        private readonly ILicenseUrlProvider _urlProvider = new NuGetLicenseUrlProvider();

        [Fact]
        public async Task GetLicenseUrlAsync()
        {
            string result = await _urlProvider.GetLicenseUrlAsync("Flurl.Http.Xml", "1.5.0");
            Assert.Equal("https://github.com/lvermeulen/Flurl.Http.Xml/blob/master/LICENSE", result);
        }
    }
}
