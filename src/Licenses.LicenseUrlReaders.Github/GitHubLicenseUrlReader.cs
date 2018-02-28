using System;
using System.Threading.Tasks;
using Flurl.Http;
using Licenses.Core;

namespace Licenses.LicenseUrlReaders.Github
{
    public class GitHubLicenseUrlReader : ILicenseUrlReader
    {
        public Task<string> GetLicenseTextAsync(string licenseUrl)
        {
            string rawUrl = new UriBuilder(licenseUrl) { Host = "raw.githubusercontent.com" }
                .Uri
                .ToString();

            return rawUrl
                .WithoutPathSegments("blob/")
                .GetStringAsync();
        }
    }
}
