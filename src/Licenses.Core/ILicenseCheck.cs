using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenses.Core
{
    public interface ILicenseCheck
    {
        IEnumerable<License> KnownLicenses { get; }
        void AddKnownLicenses(IEnumerable<License> licenses);
        void AddKnownLicense(License license);
        void RemoveKnownLicense(License license);

        IEnumerable<ILicenseAuthority> LicenseAuthorities { get; }
        void AddLicenseAuthority(ILicenseAuthority licenseAuthority);
        void RemoveLicenseAuthority(ILicenseAuthority licenseAuthority);

        Task<License> ExecuteAsync(string packageName, string version);
    }
}
