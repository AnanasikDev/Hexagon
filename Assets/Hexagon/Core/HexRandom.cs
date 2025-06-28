using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public static class HexRandom
{
    /// <summary>
    /// Returns random element from the given array with the scope of [first, last].
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T RandomElement<T>([DisallowNull] this T[] array)
    {
        Assert.IsNotNull(array);
        Assert.AreNotEqual(array.Length, 0);
        return array[Random.Range(0, array.Length)];
    }

    /// <summary>
    /// Returns random element from the given list with the scope of [first, last].
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T RandomElement<T>([DisallowNull] this List<T> list)
    {
        Assert.IsNotNull(list);
        Assert.AreNotEqual(list.Count, 0);
        return list[Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Returns random element from the given collection with the scope of [first, last]. Number of elements (count) may not be specified, then it will be calculated automatically.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T RandomElement<T>([DisallowNull] this IEnumerable<T> collection, int? count = null)
    {
        Assert.IsNotNull(collection);
        if (!count.HasValue) count = collection.GetLength();
        Assert.AreNotEqual(count, 0);
        int target = Random.Range(0, count.Value);
        return collection.ElementAt(target);
    }

    /// <summary>
    /// Returns random element from the given array with the scope of [first, last].
    /// </summary>
    public static T RandomElementWithIndex<T>([DisallowNull] this T[] array, out int index)
    {
        index = 0;
        if (array.Length == 1) return array[0];
        index = Random.Range(0, array.Length);
        return array[index];
    }

    /// <summary>
    /// Returns random element from the given list with the scope of [first, last].
    /// </summary>
    public static T RandomElementWithIndex<T>([DisallowNull] this List<T> list, out int index)
    {
        index = 0;
        if (list.Count == 1) return list[0];
        index = Random.Range(0, list.Count);
        return list[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBool(float trueBias, float falseBias)
    {
        return Random.Range(0.0f, 1.0f) < trueBias / (trueBias + falseBias);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBool(float minTrue = 0.5f)
    {
        return Random.Range(0.0f, 1.0f) > minTrue;
    }

    /// <summary>
    /// If random [0-1] > 'minPositive' then +1 is returned, otherwise -1. The less 'minPositive' value is the higher the chance of getting +1 is.
    /// </summary>
    /// <param name="minPositive">[0-1] float threshold value, above which will be returned +1</param>
    /// <returns>+1 or -1</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetSign(float minPositive = 0.5f)
    {
        return Random.Range(0.0f, 1.0f) > minPositive ? 1 : -1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetTernarSign()
    {
        float val = Random.Range(0.0f, 1.0f);
        return val > 0.6666f ? 1 : (val > 0.3333f ? 0 : -1);
    }

    public static int GetTernarSign(float negBias, float zeroBias, float posBias)
    {
        float val = Random.Range(0.0f, 1.0f);
        float len = posBias + zeroBias + negBias;
        float posThreshold = (zeroBias + negBias) / len;
        float negThreshold = zeroBias / len;
        return val > posThreshold ? 1 : (val > negThreshold ? -1 : 0);
    }

    /// <summary>
    /// Generates a random Vector3 with values between -1 and 1.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Random3D()
        => new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

    /// <summary>
    /// Generates a random Vector2 with values between -1 and 1.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Random2D()
        => new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

    public static int GetWeightedIndex(float[] segments, float sum)
    {
        float rand = Random.Range(0.0f, sum);
        float threshold = 0.0f;
        for (int i = 0; i < segments.Length; i++)
        {
            if (rand < (threshold += segments[i]))
            {
                return i;
            }
        }
        return -1;
    }

    public static int GetWeightedIndex(float[] segments)
    {
        float sum = 0.0f;
        foreach (float v in segments) sum += v;

        return GetWeightedIndex(segments, sum);
    }
}