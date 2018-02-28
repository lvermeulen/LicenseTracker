using System;
using System.Linq;

namespace Licenses.LicenseUrlReaders.Github
{
    public static class StringExtensions
    {
        public static Uri WithoutPathSegments(this Uri uri, params string[] segmentsToRemove)
        {
            var segments = uri
                .Segments
                .Where(x => !segmentsToRemove.Contains(x, StringComparer.OrdinalIgnoreCase))
                .ToArray();

            var uriBuilder = new UriBuilder(uri)
            {
                Path = string.Concat(segments)
            };

            return uriBuilder.Uri;
        }

        public static string WithoutPathSegments(this string s, params string[] segmentsToRemove) =>
            new Uri(s)
                .WithoutPathSegments(segmentsToRemove)
                .ToString();
    }
}
