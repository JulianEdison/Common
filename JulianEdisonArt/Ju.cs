using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void VoidEvent();
public delegate void StringEvent(string s);
public delegate void Vector2Event(Vector2 v);
public delegate void Vector2IntEvent(Vector2Int v);
public delegate void Collision2DEvent(Collision2D collision);

namespace JulianEdisonArt
{
    public class Ju : MonoBehaviour
    {
        /// <summary>
        /// Resets the current IEnumerator with a new one.
        /// </summary>
        /// <param name="mB"> Oftentimes 'this' will work if called from a MonoBehavior </param>
        /// <param name="currEnumerator"> An IEnumerator variable </param>
        /// <param name="newEnumerator"> Call to an IEnumerator function, e.g. RunBlink() </param>
        /// <returns> The new Coroutine</returns>
        static public Coroutine RunCoroutine(MonoBehaviour mB, IEnumerator currEnumerator, IEnumerator newEnumerator)
        {
            if (currEnumerator != null)
                mB.StopCoroutine(currEnumerator);
            currEnumerator = newEnumerator;
            return mB.StartCoroutine(currEnumerator);
        }

        /// <summary>
        /// Gets the LayerMask for the layer named "Default"
        /// </summary>
        static public LayerMask DefaultLayer => 1 << LayerMask.NameToLayer("Default");

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
        /// Shorthand for a single-line foreach loop
        /// </summary>
        static public void Each<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
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
        /// Position at given distance and angle from origin
        /// </summary>
        static public Vector2 PolarOffset(Vector2 origin, float distance, float angle)
        {
            float x = origin.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = origin.y + distance * Mathf.Sin(angle * Mathf.Deg2Rad);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Position at given distance and random angle from origin
        /// </summary>
        static public Vector2 PolarOffset(Vector2 origin, float distance)
        {
            float angle = Random(360);
            float x = origin.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = origin.y + distance * Mathf.Sin(angle * Mathf.Deg2Rad);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Int position at given distance and angle from origin
        /// </summary>
        static public Vector2Int PolarOffset(Vector2Int origin, float distance, float angle)
        {
            int x = Mathf.RoundToInt(origin.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad));
            int y = Mathf.RoundToInt(origin.y + distance * Mathf.Sin(angle * Mathf.Deg2Rad));
            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Get the angle in degrees between start and end positions
        /// </summary>
        static public float AngleBetween(Vector3 start, Vector3 end)
        {
            Vector2 tempV = end - start;
            return Mathf.Atan2(tempV.y, tempV.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Get the angle as a Vector2 between start and end positions
        /// </summary>
        static public Vector2 VectorAngleBetween(Vector3 start, Vector3 end)
        {
            return AngleToVector(AngleBetween(start, end));
        }

        /// <summary>
        /// Converts a Vector2 to degrees
        /// </summary>
        static public float VectorToAngle(Vector2 v)
        {
            return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Converts degress to a Vector2
        /// </summary>
        static public Vector2 AngleToVector(float angle)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        /// <summary>
        /// Constrains an angle in degrees between min and max values
        /// </summary>
        static public float ClampAngle(float angle, float min, float max)
        {
            if (min < 0 && max > 0 && (angle > max || angle < min)) {
                angle -= 360;
                if (angle > max || angle < min) {
                    if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                    else return max;
                }
            } else if (min > 0 && (angle > max || angle < min)) {
                angle += 360;
                if (angle > max || angle < min) {
                    if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                    else return max;
                }
            }

            if (angle < min) return min;
            else if (angle > max) return max;
            else return angle;
        }

        /// <summary>
        /// Enable/disable all Behaviours on a GameObject 
        /// </summary>
        static public void SetAllBehaviours(GameObject gO, bool on)
        {
            Each(gO.GetComponentsInChildren<Behaviour>(), b => b.enabled = false);
        }

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

        static public bool CoinFlip()
        {
            return UnityEngine.Random.Range(0, 2) == 0;
        }

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

        static public bool Between(float n, float a, float b)
        {
            return n >= a && n <= b;
        }

        /// <summary>
        /// Returns a random Vector2 with x and y values between -1 and 1
        /// </summary>
        /// <param name="mod">An optional modifier to the final result</param>
        static public Vector2 RandomUnitVector(float mod = 1)
        {
            return new Vector2(Random(-1f, 1f), Random(-1f, 1f)) * mod;
        }

        static public Vector2 ModifiedRandomUnitVector(FloatRange modRange)
        {
            return new Vector2(Random(-1f, 1f) * modRange.Random(), Random(-1f, 1f) * modRange.Random());
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

        static public bool AngleAbove(float angle1, float angle2)
        {
            float diff = 180 - angle1;
            return Modulus(angle2 + diff, 360) < 180;
        }

        static public float ReflectAngle(float angle, Vector2 axis)
        {
            Vector2 vectorAngle = AngleToVector(angle);
            vectorAngle = new Vector2(axis.x == 1 ? -vectorAngle.x : vectorAngle.x, axis.y == 1 ? -vectorAngle.y : vectorAngle.y);
            return VectorToAngle(vectorAngle);
        }

        static public int DeflectDir(Vector2 dir, Vector2 normal)
        {
            float angle = VectorToAngle(dir);
            float newAngle = VectorToAngle(Vector2.Reflect(dir.normalized, normal));
            return AngleAbove(angle, newAngle) ? -1 : 1;
        }

        static public float AngleDifference(float a1, float a2)
        {
            return 180 - Mathf.Abs(Mathf.Abs(a1 - a2) - 180);
        }

        /// <summary>
        /// Converts an integer into its roman numeral equivalent. Doesn't go above 3999
        /// </summary>
        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) return "High";
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
            return "Bad";
        }

        static public bool DoLinesIntersect(float p0_x, float p0_y, float p1_x, float p1_y, float p2_x, float p2_y, float p3_x, float p3_y, out Vector2 intersectPoint)
        {
            float s1_x, s1_y, s2_x, s2_y;
            s1_x = p1_x - p0_x; s1_y = p1_y - p0_y;
            s2_x = p3_x - p2_x; s2_y = p3_y - p2_y;

            float s, t;
            s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y);
            t = (s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1) {
                intersectPoint = new Vector2(p0_x + (t * s1_x), p0_y + (t * s1_y));
                return true;
            }

            intersectPoint = Vector2.positiveInfinity;
            return false;
        }

        static public Vector2Int GetBotLeft(Vector2Int pos1, Vector2Int pos2)
        {
            return new Vector2Int(pos1.x < pos2.x ? pos1.x : pos2.x, pos1.y < pos2.y ? pos1.y : pos2.y);
        }

        static public Vector2Int GetTopRight(Vector2Int pos1, Vector2Int pos2)
        {
            return new Vector2Int(pos1.x > pos2.x ? pos1.x : pos2.x, pos1.y > pos2.y ? pos1.y : pos2.y);
        }

        static public Vector2Int[] DrunkenWalk(Vector2Int startPos)
        {
            Vector2Int[] positions = new Vector2Int[Random(4, 16)];
            positions[0] = startPos;

            for (int n = 1; n < positions.Length; n++) {
                switch (Random(0, 4)) {
                    case 0:
                        startPos += Vector2Int.left;
                        positions[n] = startPos;
                        break;
                    case 1:
                        startPos += Vector2Int.up;
                        positions[n] = startPos;
                        break;
                    case 2:
                        startPos += Vector2Int.right;
                        positions[n] = startPos;
                        break;
                    case 3:
                        startPos += Vector2Int.down;
                        positions[n] = startPos;
                        break;
                }
            }
            return positions;
        }

        public enum CardinalRotation { None, Zero, Ninety, OneEighty, TwoSeventy }

        static public Vector2 LocalRotate90(Vector2 offset, Vector2 localPos)
        {
            Vector2 newPos = new(-localPos.y, localPos.x);
            if (offset != Vector2.zero)
                newPos += LocalRotate90(Vector2.zero, offset);
            return newPos;
        }

        static public Vector2 LocalRotate(Vector2 offset, Vector2 localPos, CardinalRotation degrees)
        {
            switch (degrees) {
                case CardinalRotation.Zero:
                    return localPos;
                case CardinalRotation.Ninety:
                    return LocalRotate90(offset, localPos);
                case CardinalRotation.OneEighty:
                    return LocalRotate90(offset, LocalRotate90(offset, localPos));
                case CardinalRotation.TwoSeventy:
                    return LocalRotate90(offset, LocalRotate90(offset, LocalRotate90(offset, localPos)));
                default:
                    return localPos;
            }
        }

        /// <summary>
        /// Returns the center position of a tile
        /// </summary>
        static public Vector2 ClampToGrid(Vector2 pos, float tileSize)
        {
            return new Vector3((int)(pos.x / tileSize) * tileSize, (int)(pos.y / tileSize) * tileSize) + new Vector3(tileSize, tileSize) / 2f;
        }

        static public Vector2Int AverageGridPos(List<Vector2Int> gridPositions)
        {
            Vector2Int total = default;

            foreach (Vector2Int pos in gridPositions) {
                total += pos;
            }

            return total / gridPositions.Count;
        }

        /// <summary>
        /// x, y: returns the closest postion of those provided. 
        /// z: returns the closest distance.
        /// </summary>
        static public Vector3 Closest(List<Vector2> positions, Vector2 to)
        {
            Vector3 closestPos = new(0, 0, Mathf.Infinity);

            foreach (Vector2 pos in positions) {
                float dist = Vector2.Distance(pos, to);
                if (dist < closestPos.z)
                    closestPos = new Vector3(pos.x, pos.y, dist);
            }

            return closestPos;
        }

        static public Vector2Int Closest(List<Vector2Int> positions, Vector2Int to)
        {
            Vector3 closestPos = new(0, 0, Mathf.Infinity);

            foreach (Vector2Int pos in positions) {
                float dist = Vector2Int.Distance(pos, to);
                if (dist < closestPos.z)
                    closestPos = new Vector3(pos.x, pos.y, dist);
            }

            return new Vector2Int((int)closestPos.x, (int)closestPos.y);
        }

        static public int ClosestIdx(List<Vector2> positions, Vector2 to)
        {
            Vector3 closestPos = new(0, 0, Mathf.Infinity);
            int closestIdx = 0;

            for (int i = 0; i < positions.Count; i++) {
                Vector2 pos = positions[i];
                float dist = Vector2.Distance(pos, to);
                if (dist < closestPos.z) {
                    closestPos = new Vector3(pos.x, pos.y, dist);
                    closestIdx = i;
                }
            }

            return closestIdx;
        }

        static public int NumEnums(System.Type type)
        {
            return type.GetEnumValues().Length;
        }

        static public void AttachParticleSystem(ParticleSystem pS, SpriteRenderer sR, BoxCollider2D boxCollider, float pSMod = 1)
        {
            pS.transform.SetParent(boxCollider.transform);
            pS.transform.localPosition = Vector3.zero;
            pS.transform.localRotation = Quaternion.identity;

            ParticleSystem.ShapeModule pSShape = pS.shape;
            pSShape.shapeType = ParticleSystemShapeType.SpriteRenderer;
            pSShape.spriteRenderer = sR;

            ParticleSystem.EmissionModule pSEmission = pS.emission;

            bool colliderWasEnabled = boxCollider.enabled;
            boxCollider.enabled = true;

            pSEmission.rateOverTime = new ParticleSystem.MinMaxCurve(boxCollider.size.x * boxCollider.size.y * pSMod);
            boxCollider.enabled = colliderWasEnabled;
        }

        /// <summary>
        /// Destroy all children of a Transform
        /// </summary>
        /// <param name="transform">The parent Transform</param>
        /// <param name="ignoreInactive">Should this avoid destroying inactive objects?</param>
        static public void KillAllChildren(Transform transform, bool ignoreInactive = false)
        {
            foreach (Transform child in transform) {
                if (!ignoreInactive || child.gameObject.activeSelf)
                    Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Destroy all children of a Transform
        /// </summary>
        /// <param name="transform">The parent Transform</param>
        /// <param name="ignoring">Specific objects to avoid destroying</param>
        static public void KillAllChildren(Transform transform, List<GameObject> ignoring)
        {
            foreach (Transform child in transform) {
                if (!ignoring.Contains(child.gameObject))
                    Destroy(child.gameObject);
            }
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