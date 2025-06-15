using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Class for static math operations
/// </summary>
public static class Hexath
{
    public const float sqrt2 = 1.41421356237f;
    public const float sqrt2half = 0.70710678118f;
    public const float sqrt3 = 1.73205080757f;

    /// <summary>
    /// Snaps the given number to the nearest float number within the given step. Rounding for float-point numbers with adjustable accuracy given as the 'step' argument.
    /// </summary>
    /// <param name="number">Number to be rounded</param>
    /// <param name="step">Accuracy of rounding, modulo of the maximum difference with the original 'number'</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SnapNumberToStep(this float number, float step)
    {
        float remainder = number % step;

        if (System.Math.Abs(remainder) < step / 2f) return number - remainder;
        else return number - remainder + step * Mathf.Sign(number);
    }

    /// <summary>
    /// Returns a point on the circumference with the given 'radius' at the given 'angle' in degrees, starting at the point (radius, 0) as in math.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 GetCirclePointDegrees(float radius, float angle)
    {
        return GetCirclePointRadians(radius, angle * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Returns a point on the circumference with the given 'radius' at the given 'angle' in radians, starting at the point (radius, 0) as in math.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 GetCirclePointRadians(float radius, float angle)
    {
        return new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
    }
    
    /// <summary>
    /// Returns random point on the circumference of the given 'radius'.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 GetRandomRingPoint(float radius)
    {
        return GetCirclePointRadians(radius, UnityEngine.Random.Range(0f, 2f * Mathf.PI));
    }

    /// <summary>
    /// Returns random point on or within the circumference of the given 'radius'.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 GetRandomCirclePoint(float radius)
    {
        return GetCirclePointRadians(Random.Range(0, radius), UnityEngine.Random.Range(0f, 2f * Mathf.PI));
    }

    /// <summary>
    /// Least common multiple of two positive numbers. An exception will be thrown if any of the input numbers are less or equal to zero.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LeastCommonMultiple(int a, int b)
    {
        Assert.IsTrue(a > 0 && b > 0);
        return a / GreatestCommonFactor(a, b) * b;
    }

    /// <summary>
    /// Greatest common divisor (aka highest common factor) of two numbers. An exception will be thrown if any of the input numbers are less or equal to zero.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GreatestCommonFactor(int a, int b)
    {
        Assert.IsTrue(a > 0 && b > 0);
        while (b != 0)
            (a, b) = (b, a % b);
        return a;
    }

    /// <summary>
    /// Least common multiple of an array of numbers. An exception will be thrown if any of the input numbers are less or equal to zero. Input array may contain any number of elements but it cannot be null.
    /// </summary>
    public static int LeastCommonMultiple([DisallowNull] int[] values)
    {
        int len = values.Length;
        if (len == 0) return 0;
        if (len == 1) return values[0];
        int result = 0;
        if (len >= 2) result = LeastCommonMultiple(values[0], values[1]);
        for (int i = 2; i < len; i++)
        {
            result = LeastCommonMultiple(result, values[i]);
        }
        return result;
    }

    /// <summary>
    /// Greatest common divisor (aka highest common factor) of an array of numbers. An exception will be thrown if any of the input numbers are less or equal to zero. Input array may contain any number of elements but it cannot be null.
    /// </summary>
    public static int GreatestCommonFactor([DisallowNull] int[] values)
    {
        int len = values.Length;
        if (len == 0) return 0;
        if (len == 1) return values[0];
        int result = 0;
        if (len >= 2) result = GreatestCommonFactor(values[0], values[1]);
        for (int i = 2; i < len; i++)
        {
            result = GreatestCommonFactor(result, values[i]);
        }
        return result;
    }

    /// <summary>
    /// Returns -1 if value is negative, 0 if value is 0, 1 if value is positive
    /// </summary>
    public static int Ternarsign(float value)
    {
        return (value > 0 ? 1 : (value < 0 ? -1 : 0));
    }

    /// <summary>
    /// Holds the input "value" at "max" when it is larger than "min", otherwise starts decreasing starting from "max".
    /// </summary>
    public static float Ramp(float value, float min, float max)
    {
        if (value > min) return max;
        return (max - min) + value;
    }

    /// <summary>
    /// Returns value if it's greater than min threshold, min otherside
    /// </summary>
    public static float MinLimit(float value, float min)
    {
        return value > min ? value : min;
    }

    /// <summary>
    /// Returns value if it's less than max threshold, or max otherwise
    /// </summary>
    public static float MaxLimit(float value, float max)
    {
        return value < max ? value : max;
    }

    public static bool NearlyEquals(this float a, float b, double epsilon = 1E-5) =>
        (a - b) <= epsilon;
}
