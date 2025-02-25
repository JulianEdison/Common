using System;

namespace JulianEdisonArt
{
    /// <summary>
    /// Represents a pair of different objects. It's kinda like a KeyValuePair but much simpler
    /// </summary>
    [Serializable]
    public class DisparatePair<T, U>
    {
        public T A;
        public U B;

        public DisparatePair() { }

        public DisparatePair(T a, U b)
        {
            A = a;
            B = b;
        }
    }
}