using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JulianEdisonArt
{
    /// <summary>
    /// A collection of functions handling random chance
    /// </summary>
    public class JuRandom
    {
        /// <summary>
        /// Get random value from a list
        /// </summary>
        static public T RandomListValue<T>(List<T> l)
        {
            return l[UnityEngine.Random.Range(0, l.Count)];
        }

        /// <summary>
        /// Get random value from a dictionary
        /// </summary>
        static public T RandomDictionaryValue<TKey, T>(IDictionary<TKey, T> dict)
        {
            return RandomListValue(dict.Values.ToList());
        }

        /// <summary>
        /// Get random key from a dictionary
        /// </summary>
        static public TKey RandomDictionaryKey<TKey, T>(IDictionary<TKey, T> dict)
        {
            return RandomListValue(dict.Keys.ToList());
        }

        /// <summary>
        /// Get random value between two floats
        /// </summary>
        static public float Random(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary>
        /// Get random value between two ints (inclusive)
        /// </summary>
        static public int Random(int min, int max)
        {
            return UnityEngine.Random.Range(min, max + 1);
        }

        /// <summary>
        /// Get random float value between 0 and max
        /// </summary>
        static public float Random(float max)
        {
            return UnityEngine.Random.Range(0, max);
        }

        /// <summary>
        /// Gets random int value between 0 and max (inclusive)
        /// </summary>
        static public int Random(int max)
        {
            return UnityEngine.Random.Range(0, max + 1);
        }

        /// <summary>
        /// Get random value between -range and range
        /// </summary>
        static public float SymmetricalRandom(float range)
        {
            return Random(-range, range);
        }

        /// <summary>
        /// Get a random position within a BoxCollider2D
        /// </summary>
        static public Vector2 RandomPointInsideBox(BoxCollider2D box)
        {
            return (Vector2)box.transform.position + box.offset + new Vector2(Random(-box.size.x / 2, box.size.x / 2), Random(-box.size.y / 2, box.size.y / 2));
        }

        /// <summary>
        /// 50% chance of true
        /// </summary>
        static public bool CoinFlip()
        {
            return UnityEngine.Random.Range(0, 2) == 0;
        }

        /// <summary>
        /// Returns either 1 or -1
        /// </summary>
        static public int RandomDir()
        {
            return CoinFlip() ? 1 : -1;
        }

        /// <summary>
        /// Between 0 - 1f
        /// </summary>
        /// <param name="max"> This should be normalized</param>
        /// <returns></returns>
        static public bool Chance(float max)
        {
            return UnityEngine.Random.Range(0, 1f) <= max;
        }

        /// <summary>
        /// Returns a random float value between 0 and 1
        /// </summary>
        static public float Random01()
        {
            return UnityEngine.Random.Range(0, 1f);
        }

        /// <summary>
        /// Returns a random Vector2 with x and y values between -1 and 1
        /// </summary>
        /// <param name="mod">An optional modifier to the final result</param>
        static public Vector2 RandomUnitVector(float mod = 1)
        {
            return new Vector2(Random(-1f, 1f), Random(-1f, 1f)) * mod;
        }

        /// <summary>
        /// Returns a random Vector2 with x and y values between -1 and 1
        /// </summary>
        /// <param name="modRange">Range to modify the final result</param>
        static public Vector2 ModifiedRandomUnitVector(FloatRange modRange)
        {
            return new Vector2(Random(-1f, 1f) * modRange.Random(), Random(-1f, 1f) * modRange.Random());
        }

        const string ALPHANUMERALS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        /// <summary>
        /// Returns a random string of letters (upper and lowercase) and numbers
        /// </summary>
        /// <param name="length">The number of characters in the string</param>
        public static string RandomAlphaNumericString(int length = 8)
        {
            string newString = default;

            for (int n = 0; n < length; n++)
                newString += ALPHANUMERALS[Random(0, ALPHANUMERALS.Length - 1)];

            return newString;
        }
    }
}
