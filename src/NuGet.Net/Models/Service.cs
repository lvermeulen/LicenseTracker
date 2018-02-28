using Newtonsoft.Json;

namespace NuGet.Net.Models
{
    public class Service
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string Comment { get; set; }
    }
}
