using System;
using System.Collections.Generic;
using System.Net.Http;
using Flurl.Http.Configuration;
using Licenses.Sources.Spdx.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace Licenses.Sources.Spdx.Tests
{
    public class SpdxLicenseSourceHttpClientFactory : DefaultHttpClientFactory
    {
        private HttpClient GetClient()
        {
            var builder = new WebHostBuilder().Configure(app =>
            {
                app.Use(async (context, next) =>
                {
                    string requestBody = context.Request.Path.ToString().EndsWith("licenses.json", StringComparison.OrdinalIgnoreCase)
                        ? JsonConvert.SerializeObject(new LicenseInfoList
                        {
                            Licenses = new List<LicenseInfo>
                            {
                                new LicenseInfo
                                {
                                    LicenseId = "mit",
                                    Name = "MIT",
                                    DetailsUrl = "http://someurl"
                                }
                            }
                        })
                        : JsonConvert.SerializeObject(new FullLicenseInfo
                        {
                            LicenseId = "mit",
                            Name = "MIT",
                            StandardLicenseTemplate = "some body"
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
