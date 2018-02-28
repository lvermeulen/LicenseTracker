using System;
using Newtonsoft.Json;

namespace Licenses.RateCheckers.GitHub.Models
{
    public class RateLimit
    {
        public int Limit { get; set; }
        public int Remaining { get; set; }
        [JsonConverter(typeof(UtcEpochSecondsConverter))]
        public DateTime Reset { get; set; }
    }
}
