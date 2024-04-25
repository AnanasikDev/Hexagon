using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Hexath
{
    public static float SnapNumberToStep(this float number, float step)
    {
        float sediment = number % step;

        if (Mathf.Abs(sediment) < step / 2f) return number - sediment;
        else return number - sediment + step * Mathf.Sign(number);
    }
    public static Vector2 GetCirclePositionDegrees(float radius, float angleDeg)
    {
        float angleRad = angleDeg * Mathf.Deg2Rad;

        return GetCirclePositionRadians(radius, angleRad);
    }
    public static Vector2 GetCirclePositionRadians(float radius, float angleRad)
    {
        float x = Mathf.Sin(angleRad) * radius;
        float y = Mathf.Cos(angleRad) * radius;

        return new Vector2(x, y);
    }
    public static Vector2 GetRandomRingPoint(float radius)
    {
        return GetCirclePositionDegrees(radius, UnityEngine.Random.Range(0f, 360f));
    }
    public static Vector2 GetRandomCirclePoint(float radius)
    {
        return GetCirclePositionDegrees(Random.Range(0, radius), UnityEngine.Random.Range(0f, 360f));
    }

    public static int HOK(int a, int b)
    {
        for (int i = a; i < b * a; i += a)
        {
            if (i % a == 0 && i % b == 0) return i;
        }

        return -1;
    }
    public static int HOD(int a, int b)
    {
        var min = a > b ? b : a;

        for (int i = min; i >= 0; i--)
        {
            if (b % i == 0 && a % i == 0) return i;
        }

        return -1;
    }

    public static int HOK(List<int> nums)
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

    public static float Ramp(float x, float min, float max)
    {
        if (x > min) return max;
        return (max - min) + x;
    }
    public static float MinLimit(float value, float min)
    {
        return value >= min ? value : min;
    }
}
