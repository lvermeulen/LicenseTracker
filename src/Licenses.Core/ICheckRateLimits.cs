using System;
using System.Threading.Tasks;

namespace Licenses.Core
{
    public interface ICheckRateLimits
    {
        Task<bool> CheckRateLimitsAsync();
        Task WithRateLimitsAsync<T>(T t, Action<T> action);
        Task<TResult> WithRateLimitsAsync<T, TResult>(T t, Func<T, Task<TResult>> selector);
    }
}
