using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public static class HexRandom
{
    /// <summary>
    /// Returns random element from the given array with the scope of [first, last].
    /// </summary>
    public static T RandomElement<T>(this T[] array)
    {
        Assert.AreNotEqual(array.Length, 0);
        return array[Random.Range(0, array.Length)];
    }

    /// <summary>
    /// Returns random element from the given list with the scope of [first, last].
    /// </summary>
    public static T RandomElement<T>(this List<T> list)
    {
        Assert.AreNotEqual(list.Count, 0);
        return list[Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Returns random element from the given collection with the scope of [first, last]. Number of elements (count) may not be specified, then it will be calculated automatically.
    /// </summary>
    public static T RandomElement<T>(this IEnumerable<T> collection, int? count = null)
    {
        if (!count.HasValue) count = collection.GetLength();
        int target = Random.Range(0, count.Value);
        return collection.ElementAt(target);
    }

    public static bool GetBool(float trueBias, float falseBias)
    {
        return Random.Range(0.0f, 1.0f) < trueBias / (trueBias + falseBias);
    }

    public static bool GetBool(float minTrue = 0.5f)
    {
        return Random.Range(0.0f, 1.0f) > minTrue;
    }

    public static int GetSign(float minPositive = 0.5f)
    {
        return Random.Range(0.0f, 1.0f) > minPositive ? 1 : -1;
    }

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
}