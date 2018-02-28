using System.Threading.Tasks;

namespace Licenses.Core
{
    public interface ILicenseUrlProvider
    {
        Task<string> GetLicenseUrlAsync(string packageName, string version);
    }
}
