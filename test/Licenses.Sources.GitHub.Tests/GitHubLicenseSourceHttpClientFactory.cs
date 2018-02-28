using System;
using System.Collections.Generic;
using System.Net.Http;
using Flurl.Http.Configuration;
using Licenses.Sources.GitHub.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace Licenses.Sources.GitHub.Tests
{
    public class GitHubLicenseSourceHttpClientFactory : DefaultHttpClientFactory
    {
        private HttpClient GetClient()
        {
            var builder = new WebHostBuilder().Configure(app =>
            {
                app.Use(async (context, next) =>
                {
                    string requestBody = context.Request.Path.ToString().EndsWith("licenses", StringComparison.OrdinalIgnoreCase)
                        ? JsonConvert.SerializeObject(new List<LicenseInfo>
                        {
                            new LicenseInfo
                            {
                                Key = "mit",
                                Name = "MIT",
                                SpdxId = "MIT",
                                Url = "http://someurl"
                            }
                        })
                        : JsonConvert.SerializeObject(new FullLicenseInfo
                        {
                            Key = "mit",
                            Name = "MIT",
                            SpdxId = "MIT",
                            Body = "some body"
                        });

                    await context.Response.WriteAsync(requestBody);
                });
            });

            var server = new TestServer(builder);
            return server.CreateClient();
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler) => GetClient();
    }
}
