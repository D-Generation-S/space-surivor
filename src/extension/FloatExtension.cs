using System;

public static class FloatExtension
{

	/// <summary>
    /// Constant for a half rotation in degree, this is identically to pi or 180 degree
    /// </summary>
    public static float HALF_ROTATION = (float)Math.PI;

    public static float Lerp(this float byValue, float minValue, float maxValue)
    {
         return minValue * (1 - byValue) + maxValue * byValue;
    }

    public static float DegreeToRadians(this float degree)
    {
        return (float)(degree * (Math.PI / 180));
    }

        public static float RadiansToDegree(this float radians)
    {
        return (float)(radians * 180/ Math.PI);
    }
}