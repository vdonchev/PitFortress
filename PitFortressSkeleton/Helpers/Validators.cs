namespace PitFortress.Helpers
{
    using System;

    public static class Validators
    {
        public static void ValidateRange(int value, int min, int max, bool inclusive = true)
        {
            if (inclusive)
            {
                if (value < min || value > max)
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                if (value <= min || value >= max)
                {
                    throw new ArgumentException();
                }
            }
        }

        public static void ValidateMinValue(int value, int min, bool inclusive = true)
        {
            if (inclusive)
            {
                if (value < min)
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                if (value <= min)
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}