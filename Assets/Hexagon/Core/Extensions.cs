using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

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
        return gameObject.TryGetComponent<T>(out T component);
    }
    public static bool HasComponent<T>(this GameObject gameObject)
    {
        return gameObject.TryGetComponent<T>(out T component);
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

    public static Vector3 GetGlobalScale(this Transform transform)
    {
        return transform.lossyScale;
    }
    public static Transform FindNearest(this Transform transform, Transform[] others, bool includeInactive = false)
    {
        return others.OrderBy(t => (t.position - transform.position).sqrMagnitude)
                              .Where(t => includeInactive ? true : t.gameObject.activeSelf).First();
    }
    public static Transform FindLocalNearest(this Transform transform, Transform[] others, bool includeInactive = false)
    {
        return others.OrderBy(t => (t.localPosition - transform.localPosition).sqrMagnitude)
                              .Where(t => includeInactive ? true : t.gameObject.activeSelf).First();
    }    
    
    public static void DoDeep(this Transform transform, Action<Transform> function)
    {
        foreach (Transform child in transform.DeepChildren())
            function(child);
    }
    public static int DeepChildrenCount(this Transform transform)
    {
        return transform.DeepChildren().Count;
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

    public static void AddScaleForwardRelative(this Transform transform, Vector3 direction)
    {
        transform.localScale += direction;
        transform.Translate(direction/2f);
    }
    public static void SetScaleForwardRelative(this Transform transform, Vector3 direction)
    {
        transform.Translate((direction - transform.localScale) / 2f);
        transform.localScale = direction;
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
    public static Vector3 GetCirclePositionRadians(float radius, float angleRad)
    {
        float x = Mathf.Sin(angleRad) * radius;
        float z = Mathf.Cos(angleRad) * radius;

        return new Vector3(x, 0, z);
    }
    public static Vector3 GetRandomCirclePosition(float radius)
    {
        return GetCirclePosition(radius, UnityEngine.Random.Range(0f, 360f));
    }
}
public static class Vector
{
    public static Vector3 Multiply(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    public static Vector3 Divide(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }
    public static Vector3 ManualPower(this Vector3 a, Vector3 b)
    {
        return new Vector3((float)System.Math.Pow(a.x, b.x), (float)System.Math.Pow(a.y, b.y), (float)System.Math.Pow(a.z, b.z));
    }
    public static Vector3 DoManual(this Vector3 a, Vector3 b, Func<float, float, float> function)
    {
        return new Vector3(function(a.x, b.x), function(a.y, b.y), function(a.z, b.z));
    }

    public static Vector3Int Multiply(this Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    public static Vector3Int Divide(this Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(a.x / b.x, a.y / b.y, a.z / b.z);
    }
    public static Vector3Int ManualPower(this Vector3Int a, Vector3Int b)
    {
        return new Vector3Int((int)System.Math.Pow(a.x, b.x), (int)System.Math.Pow(a.y, b.y), (int)System.Math.Pow(a.z, b.z));
    }
    public static Vector3Int DoManual(this Vector3Int a, Vector3Int b, Func<int, int, int> function)
    {
        return new Vector3Int(function(a.x, b.x), function(a.y, b.y), function(a.z, b.z));
    }



    public static Vector2 Multiply(this Vector2 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }
    public static Vector2 Divide(this Vector2 a, Vector2 b)
    {
        return new Vector2(a.x / b.x, a.y / b.y);
    }
    public static Vector2 ManualPower(this Vector2 a, Vector2 b)
    {
        return new Vector2((float)System.Math.Pow(a.x, b.x), (float)System.Math.Pow(a.y, b.y));
    }
    public static Vector2 DoManual(this Vector2 a, Vector2 b, Func<float, float, float> function)
    {
        return new Vector2(function(a.x, b.x), function(a.y, b.y));
    }

    public static Vector2Int Multiply(this Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x * b.x, a.y * b.y);
    }
    public static Vector2Int Divide(this Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x / b.x, a.y / b.y);
    }
    public static Vector2Int ManualPower(this Vector2Int a, Vector2Int b)
    {
        return new Vector2Int((int)System.Math.Pow(a.x, b.x), (int)System.Math.Pow(a.y, b.y));
    }
    public static Vector2Int DoManual(this Vector2Int a, Vector2Int b, Func<int, int, int> function)
    {
        return new Vector2Int(function(a.x, b.x), function(a.y, b.y));
    }
}