using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NuGet.Net.Models
{
    public class PackageRegistration
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
        [JsonProperty("@type")]
        public List<string> Type { get; set; }
        public string CatalogEntry { get; set; }
        public bool Listed { get; set; }
        public string PackageContent { get; set; }
        public DateTime Published { get; set; }
        public string Registration { get; set; }
        [JsonProperty("@context")]
        public Context Context { get; set; }
    }
}
