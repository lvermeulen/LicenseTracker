using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenses.Core
{
    public interface ILicenseSource
    {
        Task<IEnumerable<License>> GetLicensesAsync();
    }
}
