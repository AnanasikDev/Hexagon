using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Class for static math operations
/// </summary>
public static class Hexath
{
    public const float SQRT_2 = 1.4142136F;
    public const float HALF_SQRT_2 = SQRT_2 / 2.0F;
    public const float SQRT_3 = 1.7320508F;

    public const float TWO_PI = Mathf.PI * 2.0f;
    public const float HALF_PI = Mathf.PI / 2.0F;
    public const float QUARTER_PI = Mathf.PI / 4.0F;

    /// <summary>
    /// Snaps the given number to the nearest float number within the given step. Rounding for float-point numbers with adjustable accuracy given as the 'step' argument.
    /// </summary>
    /// <param name="number">Number to be rounded</param>
    /// <param name="step">Accuracy of rounding, modulo of the maximum difference with the original 'number'</param>
    public static float SnapNumberToStep(this float number, float step)
    {
        float remainder = number % step;

        if (System.Math.Abs(remainder) < step / 2f) return number - remainder;
        else return number - remainder + step * Mathf.Sign(number);
    }

    /// <summary>
    /// Returns a point on the circumference with the given 'radius' at the given 'angle' in degrees, starting at the point (radius, 0) as in math.
    /// </summary>
    public static Vector2 GetCirclePointDegrees(float radius, float angle)
    {
        return GetCirclePointRadians(radius, angle * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Returns a point on the circumference with the given 'radius' at the given 'angle' in radians, starting at the point (radius, 0) as in math.
    /// </summary>
    public static Vector2 GetCirclePointRadians(float radius, float angle)
    {
        return new Vector2(System.MathF.Cos(angle) * radius, System.MathF.Sin(angle) * radius);
    }

    /// <summary>
    /// Least common multiple of two positive numbers. An exception will be thrown if any of the input numbers are less or equal to zero.
    /// </summary>
    public static int LeastCommonMultiple(int a, int b)
    {
        Assert.IsTrue(a > 0 && b > 0);
        return a / GreatestCommonFactor(a, b) * b;
    }

    /// <summary>
    /// Least common multiple of an array of numbers. An exception will be thrown if any of the input numbers are less or equal to zero. Input array may contain any number of elements but it cannot be null.
    /// </summary>
    public static int LeastCommonMultiple(int[] values)
    {
        int len = values.Length;
        if (len == 0) return 0;
        if (len == 1) return values[0];
        int result = LeastCommonMultiple(values[0], values[1]);
        for (int i = 2; i < len; i++)
        {
            result = LeastCommonMultiple(result, values[i]);
        }
        return result;
    }

    /// <summary>
    /// Greatest common divisor (aka highest common factor) of two numbers. An exception will be thrown if any of the input numbers are less or equal to zero.
    /// </summary>
    public static int GreatestCommonFactor(int a, int b)
    {
        Assert.IsTrue(a > 0 && b > 0);
        while (b != 0)
            (a, b) = (b, a % b);
        return a;
    }

    /// <summary>
    /// Greatest common divisor (aka highest common factor) of an array of numbers. An exception will be thrown if any of the input numbers are less or equal to zero. Input array may contain any number of elements but it cannot be null.
    /// </summary>
    public static int GreatestCommonFactor(int[] values)
    {
        int len = values.Length;
        if (len == 0) return 0;
        if (len == 1) return values[0];
        int result = GreatestCommonFactor(values[0], values[1]);
        for (int i = 2; i < len; i++)
        {
            result = GreatestCommonFactor(result, values[i]);
        }
        return result;
    }

    /// <summary>
    /// Returns -1 if 'value' is negative, 0 if 'value' is 0, 1 if 'value' is positive
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Ternarsign(float value)
    {
        return (value > 0 ? 1 : (value < 0 ? -1 : 0));
    }

    /// <summary>
    /// Holds the input 'value' at 'holdValue' when it is larger than 'slideThreshold', otherwise starts decreasing starting from 'holdValue'.
    /// </summary>
    public static float Ramp(float value, float slideThreshold, float holdValue)
    {
        Assert.IsTrue(value >= 0 && slideThreshold >= 0 && holdValue >= 0);
        if (value > slideThreshold) return holdValue;
        return (holdValue - slideThreshold) + value;
    }

    /// <summary>
    /// Deprecated. Use Mathf.Max instead.
    /// Returns a value in range of [min, +INF). If input value is smaller or equal to 'min' the output will be 'min', otherwise 'value'.
    /// </summary>
    [Obsolete("Use Mathf.Max instead")]
    public static float ClampMin(this float value, float min)
    {
        return value > min ? value : min;
    }

    /// <summary>
    /// Deprecated. Use Mathf.Max instead.
    /// Returns a value in range of [min, +INF). If input value is smaller or equal to 'min' the output will be 'min', otherwise 'value'.
    /// </summary>
    [Obsolete("Use Mathf.Max instead")]
    public static float ClampMin(this int value, int min)
    {
        return value > min ? value : min;
    }

    /// <summary>
    /// Deprecated. Use Mathf.Min instead.
    /// Returns a value in range of (-INF; max]. If input value is larger or equal to 'max' the output will be 'max', otherwise 'value'.
    /// </summary>
    [Obsolete("Use Mathf.Min instead")]
    public static float ClampMax(this float value, float max)
    {
        return value < max ? value : max;
    }

    /// <summary>
    /// Deprecated. Use Mathf.Min instead.
    /// Returns a value in range of (-INF; max]. If input value is larger or equal to 'max' the output will be 'max', otherwise 'value'.
    /// </summary>
    [Obsolete("Use Mathf.Min instead")]
    public static float ClampMax(this int value, int max)
    {
        return value < max ? value : max;
    }

    /// <summary>
    /// Returns true if (a - b) no larger than epsilon, does not check signs of numbers!
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEqualsPositive(this float a, float b, double epsilon = 1E-5)
    {
        return (a - b) <= epsilon; // beware, no sign check!
    }

    /// <summary>
    /// Returns true if |a - b| no larger than epsilon, works on any numbers
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEquals(this float a, float b, double epsilon = 1E-5)
    {
        return System.MathF.Abs(a - b) <= epsilon;
    }

    /// <summary>
    /// Remaps value from one range to another. It returns a point that lies on the same relative position on the output range as on the input range.
    /// </summary>
    /// <param name="value">Value to remap</param>
    /// <param name="minFrom">Minimum value of the input range</param>
    /// <param name="maxFrom">Maximum value of the input range</param>
    /// <param name="minTo">Minimum value of the output range</param>
    /// <param name="maxTo">Maximum value of the output range</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Remap(this float value, float minFrom, float maxFrom, float minTo, float maxTo)
    {
        return minTo + (maxTo - minTo) * (value - minFrom) / (maxFrom - minFrom);
    }
}
