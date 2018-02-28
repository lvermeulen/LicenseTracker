using System;
using System.Threading.Tasks;
using Flurl.Http;
using Licenses.Core;
using Xunit;

namespace Licenses.Sources.Spdx.Tests
{
    public class SpdxLicenseSourceShould
    {
        private readonly ILicenseSource _source = new SpdxLicenseSource();

        public SpdxLicenseSourceShould()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new SpdxLicenseSourceHttpClientFactory());
        }

        [Fact]
        public async Task GetLicensesAsync()
        {
            var results = await _source.GetLicensesAsync();
            Assert.Contains(results, x => x.Key.Equals("MIT", StringComparison.OrdinalIgnoreCase));
        }
    }
}
