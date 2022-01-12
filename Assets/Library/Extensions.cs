using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

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
    public static Vector3 GlobalScale(this Transform transform)
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
    public static int DeepChildrenCount(this Transform transform)
    {
        int childcount = 0;

        void inner(Transform transform)
        {
            foreach (Transform child in transform)
            {
                childcount++;

                if (child.childCount == 0) continue;

                inner(child);
            }
        }

        inner(transform);

        return childcount;
    }
    public static List<Transform> DeepChildren(this Transform transform)
    {
        List<Transform> children = new List<Transform>();

        void inner(Transform transform)
        {
            foreach (Transform child in transform)
            {
                children.Add(child);

                if (child.childCount == 0) continue;

                inner(child);
            }
        }

        inner(transform);

        return children;
    }
}
public static class Hexath
{
    public static Vector3 GetCirclePosition(float radius, float angleDeg)
    {
        angleDeg *= Mathf.Deg2Rad;

        float x = Mathf.Sin(angleDeg) * radius;
        float z = Mathf.Cos(angleDeg) * radius;

        return new Vector3(x, 0, z);
    }
    public static Vector3 GetRandomCirclePosition(float radius)
    {
        return GetCirclePosition(radius, Random.Range(0f, 360f));
    }
}