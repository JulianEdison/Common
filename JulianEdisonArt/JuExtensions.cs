using System.Collections.Generic;
using UnityEngine;

namespace JulianEdisonArt
{
    /// <summary>
    /// A collection of extension methods
    /// </summary>
    public static class JuExtensions
    {
        /// <summary>
        /// Returns a random value from the List
        /// </summary>
        public static T Random<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Is the List empty?
        /// </summary>
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list.Count == 0;
        }

        /// <summary>
        /// Is the list not empty?
        /// </summary>
        public static bool IsNotEmpty<T>(this List<T> list)
        {
            return list.Count > 0;
        }

        /// <summary>
        /// Returns the List's first element
        /// </summary>
        public static T First<T>(this List<T> list)
        {
            return list[0];
        }

        /// <summary>
        /// Returns the List's last element
        /// </summary>
        public static T Last<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        /// <summary>
        /// Shuffles the List
        /// </summary>
        public static void Shuffle<T>(this List<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }

        /// <summary>
        /// Destroys the GameObject
        /// </summary>
        public static void Destroy(this GameObject gameObject) => UnityEngine.Object.Destroy(gameObject);

        /// <summary>
        /// Destroys all children of the Transform
        /// </summary>
        public static void KillAllChildren(this Transform container) => JuNity.KillAllChildren(container);

        /// <summary>
        /// Rounds the float to an integer
        /// </summary>
        public static int RoundToInt(this float f) => Mathf.RoundToInt(f);

        /// <summary>
        /// Constrains the int's value between a min and max
        /// </summary>
        public static int Clamp(this int n, int min, int max) => Mathf.Clamp(n, min, max);

        /// <summary>
        /// Constrains the int to a minimum of 0
        /// </summary>
        public static int MinOf0(this int n) => Mathf.Max(0, n);

        /// <summary>
        /// Constrains the int to a minimum of 1
        /// </summary>
        public static int MinOf1(this int n) => Mathf.Max(1, n);

        /// <summary>
        /// Constrains the float to a minimum of 0
        /// </summary>
        public static float MinOf0(this float f) => Mathf.Max(0, f);

        /// <summary>
        /// Constrains the float to a minimum of 1
        /// </summary>
        public static float MinOf1(this float f) => Mathf.Max(1, f);

        /// <summary>
        /// Returns the float's absolute value
        /// </summary>
        public static float Abs(this float f) => Mathf.Abs(f);

        /// <summary>
        /// Constrains the float's value between a min and max
        /// </summary>
        public static float Clamp(this float f, float min, float max) => Mathf.Clamp(f, min, max);

        /// <summary>
        /// Converts the Resolution to a Vector2
        /// </summary>
        public static Vector2 ToVector2(this Resolution res) => new(res.width, res.height);

        /// <summary>
        /// Attaches the ParticleSystem to a SpriteRenderer, copies the sprite, and adjusts emission to scale with the BoxCollider2D's size
        /// </summary>
        static public void AttachToSprite(this ParticleSystem pS, SpriteRenderer sR, BoxCollider2D boxCollider, float pSMod = 1)
        {
            pS.transform.SetParent(sR.transform);
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
        /// Destroy the Particle System by waiting for each particle to die first
        /// </summary>
        public static ParticleSystem DieOut(this ParticleSystem pS)
        {
            // Don't destroy if pS is a sub emitter
            if (pS.transform.parent && pS.transform.parent.TryGetComponent(out ParticleSystem parentPS)) {
                ParticleSystem.SubEmittersModule parentPSSub = parentPS.subEmitters;
                for (int i = 0; i < parentPSSub.subEmittersCount; i++)
                    if (parentPSSub.GetSubEmitterSystem(i) == pS) 
                        return pS;
            }

            ParticleSystem.MainModule pSMain = pS.main;
            pSMain.loop = false;
            pSMain.stopAction = ParticleSystemStopAction.Destroy;
            pS.transform.SetParent(null);

            return pS;
        }
    }
}