using UnityEngine;

namespace JulianEdisonArt
{
    [System.Serializable]
    public class IntRange
    {
        public int Min;
        public int Max;

        public event VoidEvent OnChange;
        public event VoidEvent OnMaximize;

        public IntRange()
        {
            OnChange += CheckMaximized;
        }

        public IntRange(int a, int b)
        {
            Min = a;
            Max = b;

            OnChange += CheckMaximized;
            OnChange?.Invoke();
        }

        public static IntRange operator *(IntRange range, float multiplicant)
        {
            return new IntRange((int)(range.Min * multiplicant), (int)(range.Max * multiplicant));
        }

        public void Adjust(int amt)
        {
            Min = Mathf.Clamp(Min + amt, 0, Max);
            OnChange?.Invoke();
        }

        public void Maximize()
        {
            Min = Max;
            OnChange?.Invoke();
        }

        public void Minimize()
        {
            Min = 0;
            OnChange?.Invoke();
        }

        public int Random()
        {
            return Ju.Random(Min, Max);
        }

        void CheckMaximized()
        {
            if (IsMaximized)
                OnMaximize?.Invoke();
        }

        public float Ratio => (float)Min / Max;
        public bool IsMaximized => Min >= Max;

        public override string ToString()
        {
            return Min + "/" + Max;
        }

    }
}