using System;
using System.Collections.Generic;

namespace JulianEdisonArt
{
    /// <summary>
    /// A motley collection of functions with no specific home, doomed to wander amongst the miscellany forevermore
    /// </summary>
    public class Ju
    {
        /// <summary>
        /// Shorthand for a single-line foreach loop
        /// </summary>
        static public void Each<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }

        /// <summary>
        /// Converts an integer into its roman numeral equivalent. Doesn't go above 3999 and neither should you
        /// </summary>
        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) return "SizeError";
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            return "Error";
        }
    }
}
