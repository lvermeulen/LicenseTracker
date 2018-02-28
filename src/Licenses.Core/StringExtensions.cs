using System.Linq;

namespace Licenses.Core
{
    public static class StringExtensions
    {
        public static string ReplaceAll(this string s, string[] replaceAll, string replaceWith)
        {
            return replaceAll.Aggregate(s, (current, replace) => 
                current.Replace(replace, replaceWith));
        }
    }
}
