using UnityEngine;

namespace JulianEdisonArt
{
    /// <summary>
    /// A collection of functions for angles as represented by degrees or Vector2
    /// </summary>
    public class JuAngle
    {
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
            float angle = JuRandom.Random(360);
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

        static public bool AngleAbove(float angle1, float angle2)
        {
            float diff = 180 - angle1;
            return JuMath.Modulus(angle2 + diff, 360) < 180;
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

        /// <summary>
        /// Returns the difference (in degrees) between two angles
        /// </summary>
        static public float AngleDifference(float a1, float a2)
        {
            return 180 - Mathf.Abs(Mathf.Abs(a1 - a2) - 180);
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
            return degrees switch
            {
                CardinalRotation.Zero => localPos,
                CardinalRotation.Ninety => LocalRotate90(offset, localPos),
                CardinalRotation.OneEighty => LocalRotate90(offset, LocalRotate90(offset, localPos)),
                CardinalRotation.TwoSeventy => LocalRotate90(offset, LocalRotate90(offset, LocalRotate90(offset, localPos))),
                _ => localPos,
            };
        }
    }
}
