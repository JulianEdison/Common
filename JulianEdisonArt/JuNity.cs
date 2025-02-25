using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Common delegates
public delegate void VoidEvent();
public delegate void StringEvent(string s);
public delegate void Vector2Event(Vector2 v);
public delegate void Vector2IntEvent(Vector2Int v);
public delegate void Collision2DEvent(Collision2D collision);

namespace JulianEdisonArt
{
    /// <summary>
    /// A collection of functions performed with Unity objects and structures
    /// </summary>
    public class JuNity : MonoBehaviour
    {
        /// <summary>
        /// Gets the LayerMask for the layer named "Default"
        /// </summary>
        static public LayerMask DefaultLayer => 1 << LayerMask.NameToLayer("Default");

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
        /// Enable/disable all Behaviours on a GameObject 
        /// </summary>
        static public void SetAllBehaviours(GameObject gO, bool on)
        {
            Ju.Each(gO.GetComponentsInChildren<Behaviour>(), b => b.enabled = false);
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
    }
}