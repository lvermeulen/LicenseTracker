using System;
using System.Collections.Generic;
using System.Linq;

namespace Licenses.Core
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<string> IgnoreAll(this IEnumerable<string> items, string[] linesToIgnore)
        {
            return linesToIgnore.Aggregate(items, (current, lineToIgnore) => 
                current.Where(x => x.IndexOf(lineToIgnore, StringComparison.OrdinalIgnoreCase) < 0));
        }

        public static IEnumerable<string> IgnoreBetween(this IEnumerable<string> items, string begin, string end)
        {
            int firstLineNumber = -1;
            int lastLineNumber = -1;

            var lines = items.ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                if (firstLineNumber < 0)
                {
                    firstLineNumber = lines[i].IndexOf(begin, StringComparison.OrdinalIgnoreCase) >= 0 ? i : -1;
                }

                if (lastLineNumber < 0)
                {
                    lastLineNumber = lines[i].IndexOf(end, StringComparison.OrdinalIgnoreCase) >= 0 ? i : -1;
                }
            }

            if (firstLineNumber >= 0 && (lastLineNumber < 0 || firstLineNumber < lastLineNumber))
            {
                if (lastLineNumber < 0)
                {
                    lines.RemoveRange(firstLineNumber, lines.Count - firstLineNumber);
                }
                else
                {
                    lines.RemoveRange(firstLineNumber, lastLineNumber - firstLineNumber + 1);
                }
            }

            return lines;
        }
    }
}
