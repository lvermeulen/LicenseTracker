using System.Threading.Tasks;

namespace Licenses.Core
{
    public interface ILicenseAuthority
    {
        Task<License> GetLicenseAsync(string packageName, string version);
    }
}
