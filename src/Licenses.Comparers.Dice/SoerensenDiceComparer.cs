using System;
using System.Collections.Generic;

namespace Licenses.Comparers.Dice
{
    public class SoerensenDiceComparer : IEquatable<string>
    {
        private readonly string _s;
        private readonly double _threshold;

        public SoerensenDiceComparer(string s, double threshold)
        {
            _s = s ?? throw new ArgumentNullException(nameof(s));
            _threshold = threshold;
        }

        public bool Equals(string other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            var left = new HashSet<string>();
            var right = new HashSet<string>();

            for (int i = 0; i < _s.Length - 1; ++i)
            {
                left.Add(_s.Substring(i, 2));
            }

            for (int i = 0; i < other.Length - 1; ++i)
            {
                right.Add(other.Substring(i, 2));
            }

            HashSet<string> intersection = new HashSet<string>(left);
            intersection.IntersectWith(right);

            double coefficient = 2.0 * intersection.Count / (left.Count + right.Count);
            return coefficient >= _threshold;
        }
    }
}
