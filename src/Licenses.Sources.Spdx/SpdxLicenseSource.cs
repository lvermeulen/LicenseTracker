using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Licenses.Core;
using Licenses.Sources.Spdx.Models;

namespace Licenses.Sources.Spdx
{
    public class SpdxLicenseSource : ILicenseSource
    {
        public async Task<IEnumerable<License>> GetLicensesAsync()
        {
            const string BASEURL = "https://raw.githubusercontent.com/spdx/license-list-data/master/json/licenses.json";
            const string USERAGENT_HEADER = "User-Agent";
            const string USERAGENT_VALUE = "LicenseTracker";

            var results = new List<License>();
            var list = await BASEURL
                .WithHeader(USERAGENT_HEADER, USERAGENT_VALUE)
                .GetJsonAsync<LicenseInfoList>();

            foreach (var licenseInfo in list.Licenses)
            {
                var fullLicenseInfo = await licenseInfo.DetailsUrl
                    .WithHeader(USERAGENT_HEADER, USERAGENT_VALUE)
                    .GetJsonAsync<FullLicenseInfo>();
                results.Add(new License
                {
                    Key = fullLicenseInfo.LicenseId,
                    Name = fullLicenseInfo.Name,
                    Text = fullLicenseInfo.StandardLicenseTemplate
                });
            }

            return results;
        }
    }
}
