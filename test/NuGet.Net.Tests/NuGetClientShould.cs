using System;
using System.Threading.Tasks;
using Xunit;

namespace NuGet.Net.Tests
{
    public class NuGetClientShould
    {
        private readonly NuGetClient _client;

        public NuGetClientShould()
        {
            _client = new NuGetClient();
        }

        [Fact]
        public async Task GetServicesAsync()
        {
            var results = await NuGetClient.GetServicesAsync();
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task GetPackageAsync()
        {
            var result = await _client.GetPackageAsync("Flurl.Http.Xml");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPackageVersionAsync()
        {
            var result = await _client.GetPackageVersionAsync("Flurl.Http.Xml", "1.5.0");
            Assert.NotNull(result);
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenPackageInfoIsNull()
        {
            Assert.Throws<ArgumentNullException>("packageInfo", () => _client.GetPackageVersion(null, ""));
        }

        [Fact]
        public async Task GetPackageRegistrationAsync()
        {
            var result = await _client.GetPackageRegistrationAsync("Flurl.Http.Xml", "1.5.0");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPackageLicenseUrlAsync()
        {
            var result = await _client.GetPackageLicenseUrlAsync("Flurl.Http.Xml", "1.5.0");
            Assert.NotNull(result);
        }
    }
}
