using System.Collections.Generic;

namespace Licenses.Sources.Spdx.Models
{
    public class LicenseInfoList
    {
        public string LicenseListVersion { get; set; }
        public List<LicenseInfo> Licenses { get; set; }
        public string ReleaseDate { get; set; }
    }
}
