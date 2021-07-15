using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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
        transform.ResetLocalScale();
    }
    public static void ResetLocal(this Transform transform)
    {
        transform.ResetLocalPosition();
        transform.ResetLocalRotation();
        transform.ResetLocalScale();
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
    public static void ResetLocalScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
    }
    public static Vector3 globalScale(this Transform transform)
    {
        return transform.lossyScale;
    }
    public static Transform Nearest(this Transform transform, Transform[] others, bool includeInactive = false)
    {
        return others.OrderBy(t => (t.position - transform.position).sqrMagnitude)
                              .Where(t => includeInactive ? true : t.gameObject.activeSelf).First();
    }
    public static Transform LocalNearest(this Transform transform, Transform[] others, bool includeInactive = false)
    {
        return others.OrderBy(t => (t.position - transform.position).sqrMagnitude)
                              .Where(t => includeInactive ? true : t.gameObject.activeSelf).First();
    }
    public static Vector3 GetCirclePosition(float radius)
    {
        float a = Random.Range(0f, 360f);
        return new Vector3(Mathf.Sin(a) * radius, 0, Mathf.Cos(a) * radius);
    }
}