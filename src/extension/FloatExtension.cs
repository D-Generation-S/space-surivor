using System;
using System.Data.Common;

/// <summary>
/// Extension class for all the float related methods
/// </summary>
public static class FloatExtension
{

    /// <summary>
    /// Constant for a half rotation in degree, this is identically to pi or 180 degree
    /// </summary>
    public static float HALF_ROTATION = (float)Math.PI;

    /// <summary>
    /// method to lerp between two values
    /// </summary>
    /// <param name="byValue">The value to lerp by, must be between 0 and 1</param>
    /// <param name="minValue">The minimal value for the lerp</param>
    /// <param name="maxValue">The maximal value for the lerp</param>
    /// <returns>The value after the lerp</returns>
    public static float Lerp(this float byValue, float minValue, float maxValue)
    {
        byValue = Math.Clamp(byValue, 0, 1);
        return minValue * (1 - byValue) + maxValue * byValue;
    }

    /// <summary>
    /// Method to convert a degree value to a radian value
    /// </summary>
    /// <param name="degree">The degree value to convert</param>
    /// <returns>The radians for the input degrees</returns>
    public static float DegreeToRadians(this float degree)
    {
        return (float)(degree * (Math.PI / 180));
    }

    /// <summary>
    /// Convert radians to degree
    /// </summary>
    /// <param name="radians">The radians to convert</param>
    /// <returns>The resulting degree</returns>
    public static float RadiansToDegree(this float radians)
    {
        return (float)(radians * 180/ Math.PI);
    }
}