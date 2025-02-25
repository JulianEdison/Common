using System;
using System.Collections.Generic;

namespace JulianEdisonArt
{
    /// <summary>
    /// Represents a pair of objects
    /// </summary>
    [System.Serializable]
    public class Pair<T> : IEquatable<Pair<T>>
    {
        public T A;
        public T B;

        public Pair() { }

        public Pair(T a, T b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// Get a version of the pair with objects swapped
        /// </summary>
        public Pair<T> Swapped => new(B, A);

        /// <summary>
        /// Are both object in the pair equal to each other?
        /// </summary>
        public bool AreEqual => A.Equals(B);

        public override bool Equals(object obj)
        {
            return Equals(obj as Pair<T>);
        }

        public bool Equals(Pair<T> other)
        {
            return other != null &&
                   (EqualityComparer<T>.Default.Equals(A, other.A) &&
                   EqualityComparer<T>.Default.Equals(B, other.B) ||
                   EqualityComparer<T>.Default.Equals(A, other.B) &&
                   EqualityComparer<T>.Default.Equals(B, other.A));
        }

        public override int GetHashCode()
        {
            var hashCode = -798337671;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(A);
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(B);
            return hashCode;
        }

        public static bool operator ==(Pair<T> left, Pair<T> right)
        {
            return EqualityComparer<Pair<T>>.Default.Equals(left, right);
        }

        public static bool operator !=(Pair<T> left, Pair<T> right)
        {
            return !(left == right);
        }
    }
}