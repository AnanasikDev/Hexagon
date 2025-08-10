using System.Collections.Generic;
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
    public static T GetRandomElement<T>(this IList<T> collection)
    {
        Assert.IsNotNull(collection);
        Assert.AreNotEqual(0, collection.Count, "Cannot get random element from an empty collection");
        return collection[Random.Range(0, collection.Count)];
    }

    /// <summary>
    /// Returns random element from the given collection with the scope of [first, last].
    /// </summary>
    public static T GetRandomElementWithIndex<T>(this IList<T> collection, out int index)
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
        return GetRandomElement(values);
    }

    /// <summary>
    /// Takes a specified number of elements from a collection.
    /// </summary>
    /// <param name="collection">The collection to take from.</param>
    /// <param name="num">The number of elements to take.</param>
    /// <param name="unique">If true, all returned elements will be unique. If false, elements can be chosen more than once.</param>
    /// <returns>An IEnumerable containing the chosen elements.</returns>
    public static IEnumerable<T> GetRandomElements<T>(this IList<T> collection, int num, bool unique = true)
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
                results.Add(collection.GetRandomElement());
            }
            return results;
        }
    }

    /// <summary>
    /// Shuffles the specified list in place using the Fisher-Yates algorithm.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
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

    /// <summary>
    /// Returns a new list with the elements of the original list shuffled. The original list remains unchanged.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<T> GetShuffled<T>(this IList<T> list)
    {
        List<T> result = new List<T>(list);
        result.Shuffle();
        return result;
    }

    /// <summary>
    /// Returns a random subcollection of a random length [min, max) from the given collection.
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="collection">Input collection</param>
    /// <param name="minLength">Min length (inclusive)</param>
    /// <param name="maxLength">Max length (exclusive)</param>
    /// <returns></returns>
    public static List<T> GetRandomSubcollection<T>(this IList<T> collection, int minLength, int maxLength)
    {
        Assert.IsTrue(maxLength >= minLength);
        Assert.IsTrue(maxLength <= collection.Count);
        Assert.IsTrue(minLength > 0);

        int length = Random.Range(minLength, maxLength);
        int start = Random.Range(0, collection.Count - length + 1);

        return collection.ToList().GetRange(start, length);
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
    /// Generates a random Vector2 with values between -1 and 1.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 GetVector2D()
        => new Vector2(
            UnityEngine.Random.Range(-1f, 1f), 
            UnityEngine.Random.Range(-1f, 1f));

    /// <summary>
    /// Generates a random Vector2 with values between min and max.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 GetVector2D(float min, float max)
        => new Vector2(
            UnityEngine.Random.Range(min, max), 
            UnityEngine.Random.Range(min, max));

    /// <summary>
    /// Generates a random Vector2 with values between min and max.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 GetVector2D(Vector2 min, Vector2 max)
        => new Vector2(
            UnityEngine.Random.Range(min.x, max.x), 
            UnityEngine.Random.Range(min.y, max.y));

    /// <summary>
    /// Generates a random Vector3 with values between -1 and 1.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 GetVector3D()
        => new Vector3(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f));

    /// <summary>
    /// Generates a random Vector3 with values between min and max.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 GetVector3D(float min, float max)
        => new Vector3(
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max));

    /// <summary>
    /// Generates a random Vector3 with values between min and max.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 GetVector3D(Vector3 min, Vector3 max)
        => new Vector3(
            UnityEngine.Random.Range(min.x, max.x),
            UnityEngine.Random.Range(min.y, max.y),
            UnityEngine.Random.Range(min.z, max.z));

    /// <summary>
    /// Generates a random Vector4 with values between -1 and 1.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 GetVector4D()
        => new Vector4(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f));

    /// <summary>
    /// Generates a random Vector4 with values between min and max.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 GetVector4D(float min, float max)
        => new Vector4(
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max));

    /// <summary>
    /// Generates a random Vector4 with values between min and max.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 GetVector4D(Vector4 min, Vector4 max)
        => new Vector4(
            UnityEngine.Random.Range(min.x, max.x),
            UnityEngine.Random.Range(min.y, max.y),
            UnityEngine.Random.Range(min.z, max.z),
            UnityEngine.Random.Range(min.w, max.w));

    /// <summary>
    /// Generates a random Vector2Int with values between min (inclusive) and max (exclusive).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int GetVector2Int(int min, int max)
        => new Vector2Int(
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max));

    /// <summary>
    /// Generates a random Vector2Int with values between min (inclusive) and max (exclusive).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int GetVector2Int(Vector2Int min, Vector2Int max)
        => new Vector2Int(
            UnityEngine.Random.Range(min.x, max.x),
            UnityEngine.Random.Range(min.y, max.y));

    /// <summary>
    /// Generates a random Vector3Int with values between min (inclusive) and max (exclusive).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int GetVector3Int(int min, int max)
        => new Vector3Int(
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max),
            UnityEngine.Random.Range(min, max));

    /// <summary>
    /// Generates a random Vector3Int with values between min (inclusive) and max (exclusive).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int GetVector3Int(Vector3Int min, Vector3Int max)
        => new Vector3Int(
            UnityEngine.Random.Range(min.x, max.x),
            UnityEngine.Random.Range(min.y, max.y),
            UnityEngine.Random.Range(min.z, max.z));

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

    public static float GetOverflownValue(float rangeMin, float rangeMax, float min, float max)
    {
        if (max >= min)
        {
            return Random.Range(min, max);
        }

        // If max < min, we split the range into two sections to overflow the value.
        float min1 = min; // min > max
        float max1 = rangeMax;
        float min2 = rangeMin; // start of range
        float max2 = max;

        // If first section is chosen, get random value from it
        if (GetBool(max1 - min1, max2 - min2))
        {
            return Random.Range(min1, max1);
        }
        // Otherwise, get random value from the second section
        return Random.Range(min2, max2);
    }

    #region Colors

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetColorRGBOpaque() => new Color(Random.value, Random.value, Random.value, 1f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetColorRGB() => new Color(Random.value, Random.value, Random.value, Random.value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetColorRGB(float min = 0.5f, float max = 0.9f) => new Color(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetColorHSV()
    {
        return Random.ColorHSV();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetColorHSV(float hueMin, float hueMax)
    {
        return Random.ColorHSV(hueMin, hueMax, 0f, 1f, 0f, 1f, 1f, 1f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
    {
        return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 0f, 1f, 1f, 1f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
    {
        return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, 1f, 1f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
    {
        return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);
    }

    /// <summary>
    /// Returns a random color between two colors in HSV space. Hue is overflown if hueMax overflows over the end of [0 - 1] range (is less than hueMin).
    /// </summary>
    /// <param name="c1">Min color</param>
    /// <param name="c2">Max color</param>
    /// <param name="invertHueRange">If set to true, hue range (min and max) will be inverted.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetColorHSV(Color c1, Color c2, bool invertHueRange = false)
    {
        Color.RGBToHSV(c1, out float hueMin, out float satMin, out float valueMin);
        Color.RGBToHSV(c2, out float hueMax, out float satMax, out float valueMax);

        if (invertHueRange)
        {
            (hueMin, hueMax) = (hueMax, hueMin);
        }


        if (hueMin <= hueMax)
        {
            // [0 -------- hueMin ======== hueMax -------- 1]
            return Random.ColorHSV(hueMin, hueMax, satMin, satMax, valueMin, valueMax, c1.a, c2.a);
        }

        // If hueMin > hueMax, we split the hue range into two sections to overflow the hue value.
        // [0 ======== hueMax -------- hueMin ======== 1]

        float hueMin1 = hueMin; // hueMin > hueMax
        float hueMax1 = 1f;     // end of range
        float hueMin2 = 0f;     // start of range
        float hueMax2 = hueMax; // hueMax < hueMin

        // If first section is chosen, get random color from it
        if (GetBool(hueMax1 - hueMin1, hueMax2 - hueMin2))
        {
            return Random.ColorHSV(hueMin1, hueMax1, satMin, satMax, valueMin, valueMax, c1.a, c2.a);
        }

        // Otherwise, get random color from the second section
        return Random.ColorHSV(hueMin2, hueMax2, satMin, satMax, valueMin, valueMax, c1.a, c2.a);
    }

    #endregion // Colors

    #region Random point

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 GetPointOnSphere(float radius) => Random.onUnitSphere * radius;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 GetPointInSphere(float radius) => Random.onUnitSphere * Random.Range(0, radius);

    public static Vector2 GetPointOnCircle(float radius, float maxAngle = 2 * Mathf.PI)
    {
        float angle = Random.Range(0f, maxAngle);
        return new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
    }

    public static Vector2 GetPointInCircle(float radius, float maxAngle = 2 * Mathf.PI)
    {
        float distance = Random.Range(0, radius);
        float angle = Random.Range(0f, maxAngle);
        return new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);
    }

    public static Vector3 GetPointInBounds(this Bounds bounds)
    {
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z));
    }

    public static Vector2 GetPointInRect(this Rect rect)
    {
        return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion GetRotation2D() => Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

    #endregion

    #region Enums

    public static T GetEnumValue<T>() where T : System.Enum
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
    public static char GetRandomChar(this string str)
    {
        Assert.IsFalse(string.IsNullOrEmpty(str));
        return str[Random.Range(0, str.Length)];
    }

    /// <summary>
    /// Gets a random substring of a random length from the given string.
    /// </summary>
    public static string GetRandomSubstring(this string str)
    {
        return GetRandomSubstringOfLength(str, Random.Range(1, str.Length + 1));
    }

    /// <summary>
    /// Gets a random substring of a specified length from the given string.
    /// </summary>
    public static string GetRandomSubstringOfLength(this string str, int length)
    {
        Assert.IsFalse(string.IsNullOrEmpty(str));
        Assert.IsTrue(length > 0 && length <= str.Length);
        int startIndex = Random.Range(0, str.Length - length + 1);
        return str.Substring(startIndex, length);
    }

    /// <summary>
    /// Gets a random substring of a specified length from the given string.
    /// </summary>
    public static string GetRandomSubstringOfLength(this string str, int minLength, int maxLength)
    {
        int length = Random.Range(minLength, maxLength + 1);
        return GetRandomSubstringOfLength(str, length);
    }

    #endregion // strings
}