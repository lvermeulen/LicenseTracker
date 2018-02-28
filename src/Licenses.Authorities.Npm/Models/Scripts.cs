using Newtonsoft.Json;

namespace Licenses.Authorities.Npm.Models
{
    public class Scripts
    {
        public string Test { get; set; }
        public string Testonly { get; set; }
        [JsonProperty("test-all")]
        public string Testall { get; set; }
        public string Tdd { get; set; }
        public string Lint { get; set; }
        public string Precommit { get; set; }
        public string Toc { get; set; }
        public string Build { get; set; }
        public string Release { get; set; }
    }
}