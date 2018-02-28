using System.Collections.Generic;
using Newtonsoft.Json;

namespace Licenses.Sources.GitHub.Models
{
    public class FullLicenseInfo
    {
        public string Key { get; set; }
        public string Name { get; set; }
        [JsonProperty("spdx_id")]
        public string SpdxId { get; set; }
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string Description { get; set; }
        public string Implementation { get; set; }
        public List<string> Permissions { get; set; }
        public List<string> Conditions { get; set; }
        public List<string> Limitations { get; set; }
        public string Body { get; set; }
    }
}
