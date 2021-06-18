using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public static class IntExtensions
{
    public static bool Multiples(this int a, int b)
    {
        return b != 0 && a % b == 0 && a != 0;
    }
}
public static class RandomExtensions
{
    public static int GetChance(int[] chances)
    {
        int amount = chances.Length;
        List<int> res = new List<int>();
        for (int i = 0; i < amount; i++)
        {
            for (int _ = 0; _ < chances[i]; _++) 
                res.Add(chances[i]);
        }
        return res[Random.Range(0, res.Count)];
    }
}
public static class CollectionExtensions
{
    public static object[] Add(this object[] a, object[] b)
    {
        object[] newCollection = new object[a.Length + b.Length];
        for (int i = 0; i < a.Length; i++) newCollection[i] = a[i];
        for (int i = a.Length; i < b.Length; i++) newCollection[i] = b[i];
        return newCollection;
    }
    /// <summary>
    /// Finds index of first element equals format
    /// </summary>
    /// <param name="collection"></param>
    /// <returns>index of first null element in array. If it is full than returns -1</returns>
    public static int FirstEmpty(this object[] collection, object format = null)
    {
        for (int i = 0; i < collection.Length; i++)
            if (collection[i] == format)
                return i;
        return -1;
    }
}
public static class IEnumerableExtensions
{
    public static string ToStringFormat<T>(this IEnumerable<T> collection)
    {
        return string.Join(", ", collection);
    }
}