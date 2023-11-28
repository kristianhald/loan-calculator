using System;

namespace Calculator
{
    public static class MathExtension
    {
        public static decimal RoundToNearestThousand(this decimal value)
        {
            return Math.Round(Math.Round(value, 0) / 1000, 0) * 1000;
        }
    }
}