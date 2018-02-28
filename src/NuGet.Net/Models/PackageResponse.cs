using System.Collections.Generic;

namespace NuGet.Net.Models
{
    public class PackageResponse
    {
        public int TotalHits { get; set; }
        public List<PackageInfo> Data { get; set; }
    }
}
