using UnityEngine;

namespace JulianEdisonArt
{
    /// <summary>
    /// Representation of a range between Min and Max
    /// </summary>
    [System.Serializable]
    public class FloatRange
    {
        public float Min;
        public float Max;

        public event VoidEvent OnChange;
        public event VoidEvent OnMaximize;

        public FloatRange()
        {
            OnChange += CheckMaximized;
        }

        public FloatRange(float a, float b)
        {
            Min = a;
            Max = b;

            OnChange += CheckMaximized;
            OnChange?.Invoke();
        }

        public static FloatRange operator *(FloatRange range, float multiplicant)
        {
            return new FloatRange(range.Min * multiplicant, range.Max * multiplicant);
        }

        public static FloatRange operator /(FloatRange range, float divisor)
        {
            return new FloatRange(range.Min / divisor, range.Max / divisor);
        }

        /// <summary>
        /// Set the Min and Max values directly
        /// </summary>
        public void Set(float min, float max)
        {
            Min = min;
            Max = max;
            OnChange?.Invoke();
        }

        /// <summary>
        /// Set Min to the maximum value
        /// </summary>
        public void Maximize()
        {
            Min = Max;
            OnChange?.Invoke();
        }

        /// <summary>
        /// Set Min to 0
        /// </summary>
        public void Zero()
        {
            Min = 0;
            OnChange?.Invoke();
        }

        /// <summary>
        /// Get the ratio of Min to Max
        /// </summary>
        public float Ratio => Min / Max;

        /// <summary>
        /// Adjust Min by amt, clamped between 0 and Max
        /// </summary>
        public void Adjust(float amt)
        {
            Min = Mathf.Clamp(Min + amt, 0, Max);
            OnChange?.Invoke();
        }

        /// <summary>
        /// Get a random value between Min and Max (inclusive)
        /// </summary>
        public float Random()
        {
            return JuRandom.Random(Min, Max);
        }

        void CheckMaximized()
        {
            if (IsMaximized)
                OnMaximize?.Invoke();
        }

        /// <summary>
        /// Is Min at max value?
        /// </summary>
        public bool IsMaximized => Min >= Max;

        /// <summary>
        /// Is Min not at max value?
        /// </summary>
        public bool IsNotMaximized => !IsMaximized;

        /// <summary>
        /// Is Min at 0?
        /// </summary>
        public bool IsMinimized => Min <= 0;

        /// <summary>
        /// Is Min not at 0?
        /// </summary>
        public bool IsNotMinimized => !IsMinimized;

        /// <summary>
        /// Get the difference between Min and Max. Mathf.Abs(Max - Min)
        /// </summary>
        public float Difference => Mathf.Abs(Max - Min);

        /// <summary>
        /// Is val a value between Min and Max?
        /// </summary>
        /// <param name="val">The value to check</param>
        /// <param name="minMod">A modifier to Min</param>
        /// <param name="maxMod">A modifier to Max</param>
        /// <returns></returns>
        public bool Within(float val, float minMod = 1, float maxMod = 1)
        {
            return val >= Min * minMod && val <= Max * maxMod;
        }

        public override string ToString()
        {
            return Min + " | " + Max;
        }
    }
}