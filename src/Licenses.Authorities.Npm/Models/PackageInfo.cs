using System.Collections.Generic;

namespace Licenses.Authorities.Npm.Models
{
    public class PackageInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Main { get; set; }
        public List<string> Files { get; set; }
        public Repository Repository { get; set; }
        public Author Author { get; set; }
        public string License { get; set; }
        public Bugs Bugs { get; set; }
        public string Homepage { get; set; }
        public string GitHead { get; set; }
        public string Id { get; set; }
        public string NpmVersion { get; set; }
        public string NodeVersion { get; set; }
        public List<Maintainer> Maintainers { get; set; }
    }
}
