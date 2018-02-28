using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenses.Core
{
    public class LicenseCheck : ILicenseCheck
    {
        private readonly List<License> _knownLicenses = new List<License>();
        private readonly List<ILicenseAuthority> _licenseAuthorities = new List<ILicenseAuthority>();

        private async Task<License> FindFirstPackageLicenseAsync(string packageName, string version)
        {
            return (await Task.WhenAll(_licenseAuthorities.Select(x => x.GetLicenseAsync(packageName, version))))
                .FirstOrDefault(x => x != null);
        }

        public IEnumerable<License> KnownLicenses => _knownLicenses;

        public void AddKnownLicenses(IEnumerable<License> licenses)
        {
            if (licenses == null)
            {
                throw new ArgumentNullException(nameof(licenses));
            }

            foreach (var license in licenses)
            {
                AddKnownLicense(license);
            }
        }

        public void AddKnownLicense(License license)
        {
            if (license == null)
            {
                throw new ArgumentNullException(nameof(license));
            }

            _knownLicenses.Add(license);
        }

        public void RemoveKnownLicense(License license)
        {
            if (license == null)
            {
                throw new ArgumentNullException(nameof(license));
            }

            _knownLicenses.Remove(license);
        }

        public IEnumerable<ILicenseAuthority> LicenseAuthorities => _licenseAuthorities;

        public void AddLicenseAuthority(ILicenseAuthority licenseAuthority)
        {
            if (licenseAuthority == null)
            {
                throw new ArgumentNullException(nameof(licenseAuthority));
            }

            _licenseAuthorities.Add(licenseAuthority);
        }

        public void RemoveLicenseAuthority(ILicenseAuthority licenseAuthority)
        {
            if (licenseAuthority == null)
            {
                throw new ArgumentNullException(nameof(licenseAuthority));
            }

            _licenseAuthorities.Remove(licenseAuthority);
        }

        public Task<License> ExecuteAsync(string packageName, string version)
        {
            return FindFirstPackageLicenseAsync(packageName, version);
        }
    }
}
