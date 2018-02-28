using System;
using System.Linq;
using Flurl;

namespace NuGet.Net
{
    public static class StringExtensions
    {
        public static Uri WithoutPathSegments(this Uri uri, int number)
        {
            var segments = uri
                .Segments
                .Take(uri.Segments.Length - number)
                .ToArray();

            segments[segments.Length - number] = segments[segments.Length - number].TrimEnd('/');
            var uriBuilder = new UriBuilder(uri)
            {
                Path = string.Concat(segments)
            };

            return uriBuilder.Uri;
        }

        public static string WithoutPathSegments(this string s, int number) => 
            new Uri(s)
                .WithoutPathSegments(number)
                .ToString();

        public static string WithoutLastPathSegment(this string s) => s.WithoutPathSegments(1);

        public static string ForNuspec(this string url, string packageName)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (packageName == null)
            {
                throw new ArgumentNullException(nameof(packageName));
            }

            return url
                .WithoutLastPathSegment()
                .AppendPathSegment($"/{packageName.ToLowerInvariant()}.nuspec");
        }
    }
}
