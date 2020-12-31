using System;
using Xunit;

namespace UnitTests
{
    public static class TestUtils
    {
        // Presition if based on the decimal magnitude of the expected
        public static void AssertEqualityAndSignificance(double expected, double actual)
        {
            // Surly this can be done through double binary representation manipulation.
            // Use the magnitude of the exponent after the floating point?
            string doubleString = Convert.ToString(expected);
            string[] split = doubleString.Split('.');
            string decimalString = split[1];
            int mag = decimalString.Length;

            // Significant value
            Assert.Equal(expected, actual, mag);
        }
    }
}
