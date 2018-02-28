using Newtonsoft.Json;

namespace NuGet.Net.Models
{
    public abstract class Typed
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
    }
}
