using System.Collections.Generic;
using Newtonsoft.Json;

namespace NuGet.Net.Models
{
    public class PackageInfo
    {
        [JsonProperty("@id")]
        public string AtId { get; set; }
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string Registration { get; set; }
        public string Id { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public string LicenseUrl { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Authors { get; set; }
        public int TotalDownloads { get; set; }
        public bool Verified { get; set; }
        public List<PackageVersion> Versions { get; set; }
    }
}