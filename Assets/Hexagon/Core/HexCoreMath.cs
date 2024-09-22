using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Hexath
{
    public static float SnapNumberToStep(this float number, float step)
    {
        float remainder = number % step;

        if (Mathf.Abs(remainder) < step / 2f) return number - remainder;
        else return number - remainder + step * Mathf.Sign(number);
    }
    public static Vector2 GetCirclePointDegrees(float radius, float angleDeg)
    {
        float angleRad = angleDeg * Mathf.Deg2Rad;

        return GetCirclePointRadians(radius, angleRad);
    }
    public static Vector2 GetCirclePointRadians(float radius, float angleRad)
    {
        float x = Mathf.Cos(angleRad) * radius;
        float y = Mathf.Sin(angleRad) * radius;

        return new Vector2(x, y);
    }
    public static Vector2 GetRandomRingPoint(float radius)
    {
        return GetCirclePointDegrees(radius, UnityEngine.Random.Range(0f, 360f));
    }
    public static Vector2 GetRandomCirclePoint(float radius)
    {
        return GetCirclePointDegrees(Random.Range(0, radius), UnityEngine.Random.Range(0f, 360f));
    }

    /// <summary>
    /// Least common multiple of two numbers
    /// </summary>
    public static int LCM(int a, int b)
    {
        for (int i = a; i < b * a; i += a)
        {
            if (i % a == 0 && i % b == 0) return i;
        }

        return -1;
    }
    /// <summary>
    /// Greatest common divisor of two numbers
    /// </summary>
    public static int GCD(int a, int b)
    {
        int min = a > b ? b : a;

        for (int i = min; i >= 0; i--)
        {
            if (b % i == 0 && a % i == 0) return i;
        }

        return -1;
    }

    /// <summary>
    /// Least common multiple of a list of numbers
    /// </summary>
    public static int LCM(List<int> nums)
    {
        int max = nums.Max();
        int lcm = max;

        while (true)
        {
            bool divisible = true;

            for (int i = 0; i < nums.Count; i++)
            {
                if (lcm % nums[i] != 0)
                {
                    divisible = false;
                    break;
                }
            }

            if (divisible)
            {
                return lcm;
            }

            lcm += max;
        }
    }

    /// <summary>
    /// Returns -1 if value is negative, 0 if value is 0, 1 if value is positive
    /// </summary>
    public static int Ternarsign(float value)
    {
        return (value > 0 ? 1 : (value < 0 ? -1 : 0));
    }

    public static float Ramp(float x, float min, float max)
    {
        if (x > min) return max;
        return (max - min) + x;
    }

    /// <summary>
    /// Returns min if value is less than min, or the value otherwise
    /// </summary>
    public static float MinLimit(float value, float min)
    {
        return value > min ? value : min;
    }

    /// <summary>
    /// Returns max if value is greater than max, or the value otherwise
    /// </summary>
    public static float MaxLimit(float value, float max)
    {
        return value < max ? value : max;
    }
}
