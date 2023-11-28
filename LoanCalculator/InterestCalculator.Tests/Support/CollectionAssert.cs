using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace Koolawong.InterestCalculator.Tests.Support
{
    static class CollectionAssert
    {
        public static void Equal(this IEnumerable<decimal> expected, IEnumerable<decimal> actual, decimal precision)
        {
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            if (expectedArray.Length != actualArray.Length)
                throw new EqualException(expectedArray.Length, actualArray.Length);

            for (var index = 0; index < expectedArray.Length; index++)
            {
                var expectedValue = expectedArray[index];
                var actualValue = actualArray[index];

                if (Math.Abs(expectedValue - actualValue) > precision)
                    throw new AssertActualExpectedException(expectedValue, actualValue, "At index: " + index);
            }
        }
    }
}
