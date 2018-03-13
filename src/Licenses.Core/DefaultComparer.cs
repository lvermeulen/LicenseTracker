using System;
using System.Linq;

namespace Licenses.Core
{
    public class DefaultComparer : IEquatable<string>
    {
        private readonly string _s;
        private readonly int _linesToSkip;
        private readonly string[] _wordsToIgnore;
        private readonly string[] _linesToIgnore;

        public DefaultComparer(string s, int linesToSkip = 1, string[] wordsToIgnore = null, string[] linesToIgnore = null)
        {
            _s = s;
            _linesToSkip = linesToSkip;
            _wordsToIgnore = wordsToIgnore ?? new[] { "\n", "\r", "\t", "the", " ", ",", ";", "." };
            _linesToIgnore = linesToIgnore ?? new[] { "copyright" };
        }

        public string Normalize(string s)
        {
            return string.Join("", s.ToLowerInvariant()
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(_linesToSkip)
                .IgnoreBetween("<<beginOptional>>", "<<endOptional>>")
                .IgnoreAll(_linesToIgnore))
                .ReplaceAll(_wordsToIgnore, "");
        }

        public bool Equals(string other)
        {
            return Normalize(_s) == Normalize(other);
        }
    }
}
