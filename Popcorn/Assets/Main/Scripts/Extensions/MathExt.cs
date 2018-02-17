namespace Popcorn.Extensions
{

    public static class MathExt
    {

        public static float GetPercent(float value, int percent)
        {
            return percent / 100 * value;
        }

        public static float GetInvertValue(float value)
        {
            return value * -1;
        }

    }
}
