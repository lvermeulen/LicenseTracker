using System.Collections.Generic;

namespace Licenses.Sources.Spdx.Models
{
    public class FullLicenseInfo
    {
        public bool IsDeprecatedLicenseId { get; set; }
        public string LicenseText { get; set; }
        public string StandardLicenseTemplate { get; set; }
        public string Name { get; set; }
        public string LicenseId { get; set; }
        public List<string> SeeAlso { get; set; }
        public bool IsOsiApproved { get; set; }
    }
}
