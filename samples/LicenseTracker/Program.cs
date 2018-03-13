using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Licenses.Authorities.Npm;
using Licenses.Authorities.NuGet;
using Licenses.Comparers.Dice;
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

        private static async Task ProcessLicenseAsync(string packageName, string version, IEnumerable<License> knownLicenses)
        {
            var license = await s_checker.ExecuteAsync(packageName, version);

            var comparer = new SoerensenDiceComparer(license?.Text, 0.90);
            foreach (var knownLicense in knownLicenses)
            {
                bool equal = comparer.Equals(knownLicense.Text);
                if (equal)
                {
                    Console.WriteLine($"License check: {packageName}/{version} has license {knownLicense.Name}");
                    return;
                }
            }

            Console.WriteLine($"License check: {packageName}/{version} has unknown license"); ;
        }

        public static async Task Main(string[] args)
        {
            var knownLicenses = (await LoadLicensesAsync("licenses.json")).ToList();
            s_checker.AddKnownLicenses(knownLicenses);

            s_checker.AddLicenseAuthority(new NuGetLicenseAuthority(s_checker.KnownLicenses));
            s_checker.AddLicenseAuthority(new NpmLicenseAuthority(s_checker.KnownLicenses));

            await ProcessLicenseAsync("Flurl.Http.Xml", "1.5.0", knownLicenses);
            await ProcessLicenseAsync("Yup", "0.24.1", knownLicenses);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
