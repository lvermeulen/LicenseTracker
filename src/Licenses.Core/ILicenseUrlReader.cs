using System.Threading.Tasks;

namespace Licenses.Core
{
    public interface ILicenseUrlReader
    {
        Task<string> GetLicenseTextAsync(string licenseUrl);
    }
}
