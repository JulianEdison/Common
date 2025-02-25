using UnityEngine;

namespace JulianEdisonArt
{
    /// <summary>
    /// A collection of math and logic functions
    /// </summary>
    public class JuMath
    {
        /// <summary>
        /// Smoothly reduce a value by applying a base-10 log scale, keeping the result >= 0 
        /// </summary>
        static public float Dampen(float amt)
        {
            return Mathf.Max(0, Mathf.Log10(amt));
        }

        /// <summary>
        /// Smooth a value towards 1
        /// </summary>
        static public float DampenStat(float stat, float amt = 2)
        {
            amt = 1 + (1 / amt);
            return stat + (1 - stat) / amt;
        }

        static public float StrengthenStat(float stat, float amt = 2)
        {
            return stat < 1 ? stat / amt : stat - (1 - stat) * amt;
        }

        static public float Modulus(float a, float b)
        {
            return a - b * Mathf.Floor(a / b);
        }

        static public int Factorial(int n)
        {
            if (n == 0) return (1);

            return n * Factorial(n - 1);
        }

        static public int RoundUp(int numToRound, int multiple)
        {
            if (multiple == 0)
                return numToRound;

            int remainder = numToRound % multiple;
            if (remainder == 0)
                return numToRound;

            return numToRound + multiple - remainder;
        }

        static public bool Between(float n, float a, float b)
        {
            return n >= a && n <= b;
        }

        static public int NumEnums(System.Type type)
        {
            return type.GetEnumValues().Length;
        }
    }
}
