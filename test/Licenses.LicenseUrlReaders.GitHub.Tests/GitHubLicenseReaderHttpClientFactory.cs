using System.Net.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

namespace Licenses.LicenseUrlReaders.GitHub.Tests
{
    public class GitHubLicenseReaderHttpClientFactory : DefaultHttpClientFactory
    {
        private HttpClient GetClient()
        {
            var builder = new WebHostBuilder().Configure(app =>
            {
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("some body");
                });
            });

            var server = new TestServer(builder);
            return server.CreateClient();
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler) => GetClient();
    }
}
