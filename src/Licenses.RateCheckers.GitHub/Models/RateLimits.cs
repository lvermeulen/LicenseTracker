namespace Licenses.RateCheckers.GitHub.Models
{
    public class RateLimits
    {
        public Resources Resources { get; set; }
        public RateLimit Rate { get; set; }
    }
}