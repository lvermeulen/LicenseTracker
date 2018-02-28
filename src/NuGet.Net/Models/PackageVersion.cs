using Newtonsoft.Json;

namespace NuGet.Net.Models
{
    public class PackageVersion
    {
        public string Version { get; set; }
        public int Downloads { get; set; }
        [JsonProperty("@id")]
        public string Id { get; set; }
    }
}