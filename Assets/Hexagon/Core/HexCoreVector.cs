using UnityEngine;
using System;

public static class HexVector3
{
    public static Vector3 Multiply(this Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Vector3 Divide(this Vector3 a, Vector3 b) => new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    public static Vector3 Abs(this Vector3 vector) => new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    public static Vector3 NullZ(this Vector3 vector) => new Vector3(vector.x, vector.y, 0);

    /// <summary>
    /// Rotates the given vector2D by the given degree. 
    /// </summary>
    public static Vector2 Rotate(this Vector2 vector, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
        float tx = vector.x;
        float ty = vector.y;
        vector.x = (cos * tx) - (sin * ty);
        vector.y = (sin * tx) + (cos * ty);
        return vector;
    }

    /// <summary>
    /// Calculates the distance between vector a and vector b:
    /// (a - b).sqrMagnitude
    /// </summary>
    public static float SqrDistance2D(this Vector2 a, Vector2 b) => (a - b).sqrMagnitude;
    /// <summary>
    /// Calculates the distance between vector3D a and vector3D b in 2D space (ignoring Z-axis):
    /// (a - b).sqrMagnitude
    /// </summary>
    public static float SqrDistance2D(this Vector3 a, Vector3 b) => (a - b).SqrMagnitudeXY();
    public static float SqrMagnitudeXY(this Vector3 vector) => new Vector2(vector.x, vector.y).sqrMagnitude;

    /// <summary>
    /// Returns a new vector based on input vector with modificated X value
    /// </summary>
    public static Vector3 WithX(this Vector3 vector, float x) => new Vector3(x, vector.y, vector.z);

    /// <summary>
    /// Returns a new vector based on input vector with modificated Y value
    /// </summary>
    public static Vector3 WithY(this Vector3 vector, float y) => new Vector3(vector.x, y, vector.z);

    /// <summary>
    /// Returns a new vector based on input vector with modificated Z value
    /// </summary>
    public static Vector3 WithZ(this Vector3 vector, float z) => new Vector3(vector.x, vector.y, z);

    /// <summary>
    /// Modifies the input vector with the given X value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetX(this ref Vector3 vector, float x) => vector.x = x;

    /// <summary>
    /// Modifies the input vector with the given Y value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetY(this ref Vector3 vector, float y) => vector.y = y;

    /// <summary>
    /// Modifies the input vector with the given Z value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetZ(this ref Vector3 vector, float z) => vector.z = z;

    public static Color ConvertToColor01(this Vector3 vector) => new Color(vector.x, vector.y, vector.z, 1);
    public static Vector3 ConvertToVector01(this Color color) => new Vector3(color.r, color.g, color.b);

    public static Vector2 ConvertTo2D(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);

    public static bool IsBetween(this float a, float min, float max, bool edgesInclude = false) 
        => edgesInclude ? a >= min && a <= max : a > min && a < max;
}

public static class HexVector3Int
{
    public static Vector3Int Multiply(this Vector3Int a, Vector3Int b) => new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Vector3Int Divide(this Vector3Int a, Vector3Int b) => new Vector3Int(a.x / b.x, a.y / b.y, a.z / b.z);
    public static Vector3Int Abs(this Vector3Int vector) => new Vector3Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    public static Vector3Int NullZ(this Vector3Int vector) => new Vector3Int(vector.x, vector.y, 0);
}

public static class HexVector2
{
    public static Vector2 Multiply(this Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
    public static Vector2 Divide(this Vector2 a, Vector2 b) => new Vector2(a.x / b.x, a.y / b.y);
    public static Vector2 Abs(this Vector2 vector) => new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

    public static Vector3 ConvertTo3D(this Vector2 vector2) => new Vector3(vector2.x, vector2.y);
}

public static class HexVector2Int
{
    public static Vector2Int Multiply(this Vector2Int a, Vector2Int b) => new Vector2Int(a.x * b.x, a.y * b.y);
    public static Vector2Int Divide(this Vector2Int a, Vector2Int b) => new Vector2Int(a.x / b.x, a.y / b.y);
    public static Vector2Int Abs(this Vector2Int vector) => new Vector2Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
}
