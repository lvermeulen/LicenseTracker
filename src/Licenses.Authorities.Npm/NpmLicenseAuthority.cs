using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Licenses.Authorities.Npm.Models;
using Licenses.Core;

namespace Licenses.Authorities.Npm
{
    public class NpmLicenseAuthority : ILicenseAuthority
    {
        private readonly List<License> _knownLicenses;

        public NpmLicenseAuthority(IEnumerable<License> knownLicenses)
        {
            _knownLicenses = knownLicenses.ToList();
        }

        public async Task<License> GetLicenseAsync(string packageName, string version)
        {
            const string BASEURL = "https://registry.npmjs.org";

            try
            {
                string licenseName = (await BASEURL
                        .AppendPathSegment($"{packageName.ToLowerInvariant()}")
                        .AppendPathSegment(version)
                        .GetJsonAsync<PackageInfo>())
                    .License;

                return _knownLicenses
                    .FirstOrDefault(x => x.Key == licenseName || x.Name == licenseName);
            }
            catch
            {
                return null;
            }
        }
    }
}
