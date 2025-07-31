using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public static class HexRandom
{
    #region Collections

    /// <summary>
    /// Returns random element from the given collection with the scope of [first, last].
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T RandomElement<T>([DisallowNull] this IList<T> collection)
    {
        Assert.IsNotNull(collection);
        Assert.AreNotEqual(0, collection.Count, "Cannot get random element from an empty collection");
        return collection[Random.Range(0, collection.Count)];
    }

    /// <summary>
    /// Returns random element from the given collection with the scope of [first, last].
    /// </summary>
    public static T RandomElementWithIndex<T>([DisallowNull] this IList<T> collection, out int index)
    {
        Assert.IsNotNull(collection);
        Assert.AreNotEqual(0, collection.Count, "Cannot get random element from an empty collection");

        if (collection.Count == 1)
        {
            index = 0;
            return collection[0];
        }

        index = Random.Range(0, collection.Count);
        return collection[index];
    }

    /// <summary>
    /// A convenient shorthand to select one of the provided values at random.
    /// </summary>
    /// <example>var chosenColor = HexRandom.Select(Color.red, Color.blue, Color.green);</example>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Select<T>(params T[] values)
    {
        return RandomElement(values);
    }

    /// <summary>
    /// Takes a specified number of elements from a collection.
    /// </summary>
    /// <param name="collection">The collection to take from.</param>
    /// <param name="num">The number of elements to take.</param>
    /// <param name="unique">If true, all returned elements will be unique. If false, elements can be chosen more than once.</param>
    /// <returns>An IEnumerable containing the chosen elements.</returns>
    public static IEnumerable<T> GetRandomElements<T>([DisallowNull] this IList<T> collection, int num, bool unique = true)
    {
        Assert.IsNotNull(collection);
        Assert.IsTrue(num >= 0, "Number of elements to take cannot be negative.");
        if (collection.Count == 0 || num == 0) return Enumerable.Empty<T>();

        if (unique)
        {
            Assert.IsTrue(num <= collection.Count, "Cannot take more unique elements than exist in the collection.");
            collection.Shuffle();
            return collection.Take(num);
        }
        else
        {
            var results = new List<T>(num);
            for (int i = 0; i < num; i++)
            {
                results.Add(collection.RandomElement());
            }
            return results;
        }
    }

    /// <summary>
    /// Shuffles the specified list in place using the Fisher-Yates algorithm.
    /// </summary>
    public static void Shuffle<T>([DisallowNull] this IList<T> list)
    {
        Assert.IsNotNull(list);
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public static List<T> GetShuffled<T>([DisallowNull] this IList<T> list)
    {
        List<T> result = new List<T>(list);
        result.Shuffle();
        return result;
    }

    #endregion

    #region Booleans

    /// <summary>
    /// Returns random boolean based on true/false ratio. When they are equal (i.e. 1:1) results are uniformly spread. The larger the bias towards one value is, the more frequent it will appear in the output. No checks for non-zero denominator are performed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBool(float trueBias, float falseBias)
    {
        return Random.Range(0.0f, 1.0f) < trueBias / (trueBias + falseBias);
    }

    /// <summary>
    /// Returns random boolean based on the true threshold. The lower it is the more frequent true will appear in output.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBool(float minTrue = 0.5f)
    {
        return Random.Range(0.0f, 1.0f) > minTrue;
    }

    #endregion

    #region Sign

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

    /// <summary>
    /// Returns either -1, 0 or +1 with uniform distribution among the three.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetTernarSign()
    {
        float val = Random.Range(0.0f, 1.0f);
        return val > 0.6666f ? 1 : (val > 0.3333f ? 0 : -1);
    }

    /// <summary>
    /// Returns random ternar sign (-1/0/1) based on true/false ratio. When they are equal (i.e. 1:1) results are uniformly spread. The larger the bias towards one value is, the more frequent it will appear in the output. No checks for non-zero denominator are performed.
    /// </summary>
    /// <param name="negBias"></param>
    /// <param name="zeroBias"></param>
    /// <param name="posBias"></param>
    /// <returns></returns>
    public static int GetTernarSign(float negBias, float zeroBias, float posBias)
    {
        float val = Random.Range(0.0f, 1.0f);
        float len = posBias + zeroBias + negBias;
        float posThreshold = (zeroBias + negBias) / len;
        float negThreshold = zeroBias / len;
        return val > posThreshold ? 1 : (val > negThreshold ? -1 : 0);
    }
    #endregion // sign

    #region Vectors
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

    #endregion

    #region Weighted

    /// <summary>
    /// Returns index of a segment which a random value fell into. Random value is generated [0-sum]. Larger segments naturally have higher result frequency than smaller ones. Used for controlled randomness ratios.
    /// </summary>
    public static int GetWeightedIndex(float[] segments, float sum)
    {
        Assert.IsNotNull(segments);
        Assert.AreNotEqual(segments.Length, 0);
        if (segments.Length == 1) return 0;

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

    /// <summary>
    /// Returns index of a segment which a random value fell into. Random value is generated [0-sum]. Sum is calculated automatically. Larger segments naturally have higher result frequency than smaller ones. Used for controlled randomness ratios.
    /// </summary>
    public static int GetWeightedIndex(float[] segments)
    {
        Assert.IsNotNull(segments);
        Assert.AreNotEqual(segments.Length, 0);
        if (segments.Length == 1) return 0;

        float sum = 0.0f;
        foreach (float v in segments) sum += v;

        return GetWeightedIndex(segments, sum);
    }

    #endregion

    #region Colors

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color RandomColorOpaque() => new Color(Random.value, Random.value, Random.value, 1f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color RandomColor(bool randomAlpha = false) => new Color(Random.value, Random.value, Random.value, randomAlpha ? Random.value : 1f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color RandomColor(float min = 0.5f, float max = 0.9f) => new Color(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));

    #endregion // Colors

    #region Random point

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 RandomPointOnSphere(float radius) => Random.onUnitSphere * radius;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 RandomPointInSphere(float radius) => Random.onUnitSphere * Random.Range(0, radius);

    public static Vector2 RandomPointOnCircle(float radius, float maxAngle = 2 * Mathf.PI)
    {
        float angle = Random.Range(0f, maxAngle);
        return new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
    }

    public static Vector2 RandomPointInCircle(float radius, float maxAngle = 2 * Mathf.PI)
    {
        float distance = Random.Range(0, radius);
        float angle = Random.Range(0f, maxAngle);
        return new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);
    }

    public static Vector3 RandomPointInBounds(this Bounds bounds)
    {
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z));
    }

    public static Vector2 RandomPointInRect(this Rect rect)
    {
        return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion RandomRotation2D() => Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    #endregion

    #region Enums

    public static T RandomEnumValue<T>() where T : System.Enum
    {
        var values = System.Enum.GetValues(typeof(T));
        Assert.AreNotEqual(values.Length, 0, "Enum must not be empty.");
        return (T)values.GetValue(Random.Range(0, values.Length));
    }

    #endregion

    #region Strings

    /// <summary>
    /// Gets a random character from the given string.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static char GetRandomChar([DisallowNull] this string str)
    {
        Assert.IsFalse(string.IsNullOrEmpty(str));
        return str[Random.Range(0, str.Length)];
    }

    /// <summary>
    /// Gets a random substring of a random length from the given string.
    /// </summary>
    public static string GetRandomSubstring([DisallowNull] this string str)
    {
        Assert.IsFalse(string.IsNullOrEmpty(str));
        int startIndex = Random.Range(0, str.Length);
        int endIndex = Random.Range(startIndex, str.Length);
        return str.Substring(startIndex, endIndex - startIndex);
    }

    /// <summary>
    /// Gets a random substring of a specified length from the given string.
    /// </summary>
    public static string GetRandomSubstringOfLength([DisallowNull] this string str, int length)
    {
        Assert.IsFalse(string.IsNullOrEmpty(str));
        Assert.IsTrue(length > 0 && length <= str.Length);
        int startIndex = Random.Range(0, str.Length - length + 1);
        return str.Substring(startIndex, length);
    }

    #endregion // strings
}