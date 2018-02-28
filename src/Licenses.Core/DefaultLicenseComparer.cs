using System;
using System.Linq;

namespace Licenses.Core
{
    public class DefaultLicenseComparer : ILicenseComparer
    {
        private readonly int _linesToSkip;
        private readonly string[] _wordsToIgnore;
        private readonly string[] _linesToIgnore;

        public DefaultLicenseComparer(int linesToSkip = 1, string[] wordsToIgnore = null, string[] linesToIgnore = null)
        {
            _linesToSkip = linesToSkip;
            _wordsToIgnore = wordsToIgnore ?? new[] { "\n", "\r", "\t", "the", " ", ",", ";", "." };
            _linesToIgnore = linesToIgnore ?? new[] { "copyright" };
        }

        private string Normalize(string s)
        {
            return string.Join("", s.ToLowerInvariant()
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(_linesToSkip)
                .IgnoreBetween("<<beginOptional>>", "<<endOptional>>")
                .IgnoreAll(_linesToIgnore))
                .ReplaceAll(_wordsToIgnore, "");
        }

        public bool LicensesEqual(string left, string right)
        {
            return Normalize(left) == Normalize(right);
        }
    }
}
