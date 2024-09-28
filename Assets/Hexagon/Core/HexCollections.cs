using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for extensions over collections, such as lists, arrays or IEnumerable-s
/// </summary>
public static class HexCollections
{
    /// <summary>
    /// Returns random element from the given array with the scope of [first, last].
    /// </summary>
    public static T RandomElement<T>(this T[] array)
    {
        if (array.Length == 1) return array[0];
        return array[Random.Range(0, array.Length)];
    }

    /// <summary>
    /// Returns random element from the given list with the scope of [first, last].
    /// </summary>
    public static T RandomElement<T>(this List<T> list)
    {
        if (list.Count == 1) return list[0];
        return list[Random.Range(0, list.Count)];
    }
}