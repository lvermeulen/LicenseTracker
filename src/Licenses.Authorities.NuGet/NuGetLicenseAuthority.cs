using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Licenses.Core;
using Licenses.LicenseUrlProviders.NuGet;
using Licenses.LicenseUrlReaders.Github;

[assembly:InternalsVisibleTo("Licenses.Authorities.NuGet.Tests")]

namespace Licenses.Authorities.NuGet
{
    public class NuGetLicenseAuthority : ILicenseAuthority
    {
        private readonly IList<License> _knownLicenses;
        private readonly ILicenseUrlProvider _provider = new NuGetLicenseUrlProvider();
        private readonly ILicenseUrlReader _reader = new GitHubLicenseUrlReader();

        public NuGetLicenseAuthority(IEnumerable<License> knownLicenses)
        {
            _knownLicenses = knownLicenses.ToList();
        }

        internal NuGetLicenseAuthority(ILicenseUrlReader reader)
        {
            _reader = reader;
        }

        public async Task<License> GetLicenseAsync(string packageName, string version)
        {
            string licenseUrl = await _provider.GetLicenseUrlAsync(packageName, version);
            if (licenseUrl == null)
            {
                return null;
            }

            string licenseText = await _reader.GetLicenseTextAsync(licenseUrl);
            if (licenseText == null)
            {
                return null;
            }

            return _knownLicenses
                .FirstOrDefault(x => new DefaultComparer(licenseText).Equals(x.Text));
        }
    }
}
