using System.Collections.Generic;

namespace NuGet.Net.Models
{
    public class ServiceResponse
    {
        public string Version { get; set; }
        public List<Service> Resources { get; set; }
    }
}
