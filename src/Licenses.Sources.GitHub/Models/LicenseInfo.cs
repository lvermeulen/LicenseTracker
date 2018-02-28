using Newtonsoft.Json;

namespace Licenses.Sources.GitHub.Models
{
    public class LicenseInfo
    {
        public string Key { get; set; }
        public string Name { get; set; }
        [JsonProperty("spdx_id")]
        public string SpdxId { get; set; }
        public string Url { get; set; }
    }
}
