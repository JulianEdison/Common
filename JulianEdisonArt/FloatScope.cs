using UnityEngine;

namespace JulianEdisonArt
{
    [System.Serializable]
    public class FloatScope
    {
        /// <summary>
        /// The minimum value
        /// </summary>
        public float Min;
        /// <summary>
        /// The current value
        /// </summary>
        public float Value;
        /// <summary>
        /// The maximum value
        /// </summary>
        public float Max;

        public event VoidEvent OnValueChanged;

        public FloatScope() { }

        public FloatScope(float min, float max, float value)
        {
            Min = min;
            Max = max;
            Value = value;
        }

        /// <summary>
        /// Set Value to the maximum
        /// </summary>
        public void Maximize()
        {
            Value = Max;
            OnValueChanged?.Invoke();
        }

        /// <summary>
        /// Set Value to the minimum
        /// </summary>
        public void Minimize()
        {
            Value = Min;
            OnValueChanged?.Invoke();
        }

        public float Ratio()
        {
            float denominator = Max - Min;

            if (denominator == 0) return 0;

            return (Value - Min) / denominator;
        }

        public float RatioFromZero()
        {
            return Value / Max;
        }

        public float NegativeRatio()
        {
            if (Value >= 0 || Min >= 0) return 0;

            return Value / Min;
        }

        /// <summary>
        /// Adjust Value by amt, clamped between Min and Max
        /// </summary>
        public void Adjust(float amt)
        {
            Value = Mathf.Clamp(Value + amt, Min, Max);
            OnValueChanged?.Invoke();
        }

        /// <summary>
        /// Get a random value between Min and Max (inclusive)
        /// </summary>
        public float Random()
        {
            return Ju.Random(Min, Max);
        }

        /// <summary>
        /// Is Value at max?
        /// </summary>
        public bool IsMaximized => Value >= Max;
        /// <summary>
        /// Is Value at minimum?
        /// </summary>
        public bool IsMinimized => Value <= Min;
    }
}