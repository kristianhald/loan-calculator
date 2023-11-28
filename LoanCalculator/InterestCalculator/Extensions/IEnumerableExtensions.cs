using System;
using System.Collections.Generic;
using System.Linq;

namespace Koolawong.InterestCalculator.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExtensions
    {
        public static Boolean IsEmpty<T>(this IEnumerable<T> sequence)
        {
            return !sequence.Any();
        }
    }
}