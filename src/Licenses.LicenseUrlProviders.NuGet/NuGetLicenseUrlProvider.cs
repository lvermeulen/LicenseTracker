using System.Threading.Tasks;
using Licenses.Core;
using NuGet.Net;

namespace Licenses.LicenseUrlProviders.NuGet
{
    public class NuGetLicenseUrlProvider : ILicenseUrlProvider
    {
        private readonly NuGetClient _nuget = new NuGetClient();

        public Task<string> GetLicenseUrlAsync(string packageName, string version)
        {
            return _nuget.GetPackageLicenseUrlAsync(packageName, version);
        }
    }
}
