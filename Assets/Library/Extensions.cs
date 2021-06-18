using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public static class IntExtensions
{

    public static bool Multiples(this int a, int b)
    {
        return b != 0 && a % b == 0 && a != 0;
    }


    public static bool Even(this int a)
    {
        return a % 2 == 0;
    }
    public static bool EvenNo0(this int a)
    {
        return a % 2 == 0 && a != 0;
    }


    public static bool Whole(this int a)
    {
        return a % 1 == 0;
    }
    public static bool Whole(this short a)
    {
        return a % 1 == 0;
    }
    public static bool Whole(this float a)
    {
        return Mathf.Abs(a % 1) <= (double.Epsilon * 100);
    }
    public static bool Whole(this double a)
    {
        return Mathf.Abs((float)a % 1) <= (double.Epsilon * 100);
    }
    public static bool Whole(this decimal a)
    {
        return a % 1 == 0;
    }


    /// <summary>
    /// Does the range of numbers between n and m (exclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InBounds(this int a, int n, int m)
    {
        return a > n && a < m;
    }
    /// <summary>
    /// Does the range of numbers between n and m (exclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InBounds(this float a, float n, float m)
    {
        return a > n && a < m;
    }
    /// <summary>
    /// Does the range of numbers between n and m (exclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InBounds(this double a, double n, double m)
    {
        return a > n && a < m;
    }
    /// <summary>
    /// Does the range of numbers between n and m (exclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InBounds(this decimal a, decimal n, decimal m)
    {
        return a > n && a < m;
    }
    /// <summary>
    /// Does the range of numbers between n and m (exclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InBounds(this short a, short n, short m)
    {
        return a > n && a < m;
    }



    /// <summary>
    /// Does the range of numbers between n and m (inclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InOnBounds(this int a, int n, int m)
    {
        return a >= n && a <= m;
    }
    /// <summary>
    /// Does the range of numbers between n and m (inclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InOnBounds(this float a, float n, float m)
    {
        return a >= n && a <= m;
    }
    /// <summary>
    /// Does the range of numbers between n and m (inclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InOnBounds(this double a, double n, double m)
    {
        return a >= n && a <= m;
    }
    /// <summary>
    /// Does the range of numbers between n and m (inclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InOnBounds(this decimal a, decimal n, decimal m)
    {
        return a >= n && a <= m;
    }
    /// <summary>
    /// Does the range of numbers between n and m (inclusive) contain a?
    /// </summary>
    /// <returns>true if does, overwise false</returns>
    public static bool InOnBounds(this short a, short n, short m)
    {
        return a >= n && a <= m;
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
    /// <param name="format">defalt value. null or 0 or '' or "" etc.</param>
    /// <returns>index of first null element in array. If it is full than returns -1</returns>
    public static int First(this object[] collection, object format = null)
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
    public static string ToStringFormat<T>(this IEnumerable<T> collection, string separator)
    {
        return string.Join(separator, collection);
    }
}