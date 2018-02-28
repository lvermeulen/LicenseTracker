using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Licenses.Authorities.Npm;
using Licenses.Authorities.NuGet;
using Licenses.Core;
using Licenses.Sources.GitHub;
using Newtonsoft.Json;

namespace LicenseTracker
{
    public static class Program
    {
        private static readonly ILicenseSource s_source = new GitHubLicenseSource();
        private static readonly ILicenseCheck s_checker = new LicenseCheck();

        private static async Task<IEnumerable<License>> LoadLicensesAsync(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    var licenses = await s_source.GetLicensesAsync();
                    File.WriteAllText(fileName, JsonConvert.SerializeObject(licenses));
                }

                return JsonConvert.DeserializeObject<IEnumerable<License>>(File.ReadAllText(fileName));
            }
            catch
            {
                return Enumerable.Empty<License>();
            }
        }

        public static async Task Main(string[] args)
        {
            var licenses = await LoadLicensesAsync("licenses.json");
            s_checker.AddKnownLicenses(licenses);

            s_checker.AddLicenseAuthority(new NuGetLicenseAuthority(s_checker.KnownLicenses));
            s_checker.AddLicenseAuthority(new NpmLicenseAuthority(s_checker.KnownLicenses));

            var license = await s_checker.ExecuteAsync("Flurl.Http.Xml", "1.5.0");
            Console.WriteLine($"License check: {license?.Name ?? "unknown"} - Flurl.Http.Xml/1.5.0");

            license = await s_checker.ExecuteAsync("Yup", "0.24.1");
            Console.WriteLine($"License check: {license?.Name ?? "unknown"} - Yup/0.24.1");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
