﻿using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Class for extensions over collections, such as lists, arrays or IEnumerable-s
/// </summary>
public static class HexCollections
{
    /// <summary>
    /// Checks if two lists are completely equal (same length, same objects, and same order).
    /// </summary>
    public static bool AreListsEqual<T>(this List<T> list1, List<T> list2)
    {
        if (list1.Count != list2.Count) return false;

        for (int i = 0; i < list1.Count; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(list1[i], list2[i]))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Checks if two lists contain the same unique objects, regardless of order.
    /// </summary>
    public static bool AreSetsEqual<T>(this List<T> list1, List<T> list2)
    {
        var set1 = new HashSet<T>(list1);
        var set2 = new HashSet<T>(list2);

        return set1.SetEquals(set2);
    }

    /// <summary>
    /// Checks if two lists contain the same objects with the same number of occurrences, regardless of order.
    /// </summary>
    public static bool AreMultisetsEqual<T>(this List<T> list1, List<T> list2)
    {
        if (list1.Count != list2.Count) return false;

        var dict1 = list1.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        var dict2 = list2.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

        return dict1.Count == dict2.Count && !dict1.Except(dict2).Any();
    }

    /// <summary>
    /// Returns index of the first element in the collection which is null. If it is empty or all elements are not null, the return value is -1.
    /// </summary>
    public static int? FirstNullIndex<T>(this IEnumerable<T> collection)
    {
        int index = 0;
        foreach (T item in collection)
        {
            index++;
            if (item == null)
                return index;
        }
        return null;
    }

    /// <summary>
    /// Returns index of the first element in the collection which is not null. If it is empty or all elements are null, the return value is -1.
    /// </summary>
    public static int? FirstNotNullIndex<T>(this IEnumerable<T> collection)
    {
        int index = 0;
        foreach (var item in collection)
        {
            index++;
            if (item != null) return index;
        }
        return null;
    }

    public static int GetLength<T>(this IEnumerable<T> collection)
    {
        int len = 0;
        foreach (T _ in collection) len++;
        return len;
    }

    public static bool AddIfNew<T>(this ICollection<T> list, T item)
    {
        if (list.Contains(item)) return false;
        list.Add(item);
        return true;
    }
}