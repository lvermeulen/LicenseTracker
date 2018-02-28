using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Licenses.Core;
using Licenses.Sources.GitHub.Models;

namespace Licenses.Sources.GitHub
{
    public class GitHubLicenseSource : ILicenseSource
    {
        public async Task<IEnumerable<License>> GetLicensesAsync()
        {
            const string BASEURL = "https://api.github.com/licenses";
            const string USERAGENT_HEADER = "User-Agent";
            const string USERAGENT_VALUE = "LicenseTracker";

            var results = new List<License>();
            var list = await BASEURL
                .WithHeader(USERAGENT_HEADER, USERAGENT_VALUE)
                .GetJsonAsync<IEnumerable<LicenseInfo>>();

            foreach (var licenseInfo in list)
            {
                var fullLicenseInfo = await licenseInfo.Url
                    .WithHeader(USERAGENT_HEADER, USERAGENT_VALUE)
                    .GetJsonAsync<FullLicenseInfo>();
                results.Add(new License
                {
                    Key = fullLicenseInfo.SpdxId,
                    Name = fullLicenseInfo.Name,
                    Text = fullLicenseInfo.Body
                });
            }

            return results;
        }
    }
}
