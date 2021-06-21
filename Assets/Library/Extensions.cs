using UnityEngine;
using System.Linq;
using System.Collections.Generic;
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
    public static double NormalDistribution(float min, float max, float std_deviation = 2, float mean = 0)
    {
        /*float mean = ((max + min) / 2.0f) + hor_mean;
        float stdDev = std_deviation;
        float u1 = 1.0f - Random.Range(0f, 0.9999f); //uniform(0,1] random doubles
        float u2 = 1.0f - Random.Range(0f, 0.9999f);
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1) * Mathf.Sin(2.0f * Mathf.PI * u2)); //random normal(0,1)
        float randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
        return randNormal;*/

        System.Random rand = new System.Random(); //reuse this if you are generating many
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = System.Math.Sqrt(-2.0f * System.Math.Log(u1)) *
                     System.Math.Sin(2.0f * System.Math.PI * u2); //random normal(0,1)
        double randNormal =
                     mean + std_deviation * randStdNormal; //random normal(mean,stdDev^2)

        return randNormal;
    }
}
public static class IEnumerableExtensions
{
    public static string Join<T>(this string separator, IEnumerable<T> collection)
    {
        return string.Join(separator, collection);
    }
    public static string Join<T>(this IEnumerable<T> collection, string separator)
    {
        return string.Join(separator, collection);
    }
}
public static class GameObjectExtensions
{
    public static bool HasComponent<T>(this GameObject gameObject, T type)
    {
        T component;
        return gameObject.TryGetComponent<T>(out component);
    }
}
public static class TransformExtensions
{
    public static void Reset(this Transform transform)
    {
        transform.ResetPosition();
        transform.ResetRotation();
        transform.ResetScale();
    }
    public static void ResetPosition(this Transform transform)
    {
        transform.position = Vector3.zero;
    }
    public static void ResetLocalPosition(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
    }
    public static void ResetRotation(this Transform transform)
    {
        transform.rotation = Quaternion.identity;
    }
    public static void ResetLocalRotation(this Transform transform)
    {
        transform.localRotation = Quaternion.identity;
    }
    public static void ResetScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
    }
    public static Vector3 globalScale(this Transform transform)
    {
        return transform.lossyScale;
    }
}