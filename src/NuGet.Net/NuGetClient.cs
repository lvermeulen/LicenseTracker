using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Flurl;
using Flurl.Http;
using Flurl.Http.Xml;
using NuGet.Net.Models;

namespace NuGet.Net
{
    public class NuGetClient
    {
        private static IDictionary<string, Service> s_serviceMap;

        private static async Task<IDictionary<string, Service>> NeedServiceMapAsync()
        {
            return s_serviceMap ?? (s_serviceMap = await GetServicesAsync());
        }

        public static async Task<IDictionary<string, Service>> GetServicesAsync(string serviceIndex = "https://api.nuget.org/v3/index.json")
        {
            return (await serviceIndex.GetJsonAsync<ServiceResponse>())
                .Resources
                .Select(service => new { ServiceName = service.Type.Split('/').First(), Service = service })
                .GroupBy(service => service.ServiceName)
                .Select(group => new { group.Key, Services = group.OrderByDescending(x => x.Service.Type.Split('/').Last()) })
                .ToDictionary(group => group.Key, group => group.Services.First().Service);
        }

        public async Task<PackageInfo> GetPackageAsync(string packageName, bool includePreRelease = true, string semVerLevel = "2.0.0")
        {
            var serviceMap = await NeedServiceMapAsync();
            string url = serviceMap["SearchQueryService"].Id;

            var queryParamValues = new Dictionary<string, object>
            {
                ["q"] = packageName,
                ["prerelease"] = includePreRelease,
                ["semVerLevel"] = semVerLevel
            };

            return (await url
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<PackageResponse>())
                .Data
                .FirstOrDefault();
        }

        public PackageVersion GetPackageVersion(PackageInfo packageInfo, string version)
        {
            if (packageInfo == null)
            {
                throw new ArgumentNullException(nameof(packageInfo));
            }

            return packageInfo
                .Versions
                .FirstOrDefault(x => x.Version.Equals(version, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<PackageVersion> GetPackageVersionAsync(string packageName, string version)
        {
            var packageInfo = await GetPackageAsync(packageName);
            if (packageInfo == null)
            {
                return null;
            }

            return GetPackageVersion(packageInfo, version);
        }

        public async Task<PackageRegistration> GetPackageRegistrationAsync(string packageName, string version)
        {
            var packageVersion = await GetPackageVersionAsync(packageName, version);
            if (packageVersion == null)
            {
                return null;
            }

            return await packageVersion.Id
                .GetJsonAsync<PackageRegistration>();
        }

        public async Task<string> GetPackageLicenseUrlAsync(string packageName, string version)
        {
            var registration = await GetPackageRegistrationAsync(packageName, version);
            if (registration == null)
            {
                return null;
            }

            var result = await registration.PackageContent
                .ForNuspec(packageName)
                .GetXDocumentAsync();

            XNamespace xmlns = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd";
            string licenseUrl = result
                ?.Element(xmlns + "package")
                ?.Element(xmlns + "metadata")
                ?.Element(xmlns + "licenseUrl")
                ?.Value;

            return licenseUrl;
        }
    }
}
