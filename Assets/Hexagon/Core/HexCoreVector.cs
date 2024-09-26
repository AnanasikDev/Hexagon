using UnityEngine;
using System;

public static class HexVector3
{
    public static Vector3 Multiply(this Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Vector3 Divide(this Vector3 a, Vector3 b) => new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    public static Vector3 Abs(this Vector3 vector) => new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));

    /// <summary>
    /// Returns new Vector3 with nullified Z value. Does not modify the original vector.
    /// </summary>
    public static Vector3 NullZ(this Vector3 vector) => new Vector3(vector.x, vector.y, 0);

    /// <summary>
    /// Calculates the distance between vector3D a and vector3D b in 2D space (ignoring Z-axis):
    /// (a - b).sqrMagnitude
    /// </summary>
    public static float SqrDistanceXY(this Vector3 a, Vector3 b) => (a - b).NullZ().sqrMagnitude;

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

    public static Vector3 Random() 
        => new Vector3(
                       UnityEngine.Random.value - 0.5f, 
                       UnityEngine.Random.value - 0.5f, 
                       UnityEngine.Random.value - 0.5f
                      ) * 2;

    public static Vector3 Random01() 
        => new Vector3(
                       UnityEngine.Random.value,
                       UnityEngine.Random.value,
                       UnityEngine.Random.value
                       );

    public static Vector3 RandomRange(Vector3 min, Vector3 max) 
        => new Vector3(
                       UnityEngine.Random.Range(min.x, max.x),
                       UnityEngine.Random.Range(min.y, max.y),
                       UnityEngine.Random.Range(min.z, max.z)
                       );

    /// <summary>
    /// Determines if two Vector2 instances are nearly equal based on inaccuracy tolerance.
    /// </summary>
    public static bool NearlyEquals(this Vector3 a, Vector3 b, double inaccuracy = 9.0E-11) =>
        Vector3.SqrMagnitude(a - b) < inaccuracy;
}

public static class HexVector3Int
{
    public static Vector3Int Multiply(this Vector3Int a, Vector3Int b) => new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Vector3Int Divide(this Vector3Int a, Vector3Int b) => new Vector3Int(a.x / b.x, a.y / b.y, a.z / b.z);
    public static Vector3Int Abs(this Vector3Int vector) => new Vector3Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    public static Vector3Int NullZ(this Vector3Int vector) => new Vector3Int(vector.x, vector.y, 0);

    /// <summary>
    /// Returns a new Vector3Int based on input vector with modified X value
    /// </summary>
    public static Vector3Int WithX(this Vector3Int vector, int x) => new Vector3Int(x, vector.y, vector.z);

    /// <summary>
    /// Returns a new Vector3Int based on input vector with modified Y value
    /// </summary>
    public static Vector3Int WithY(this Vector3Int vector, int y) => new Vector3Int(vector.x, y, vector.z);

    /// <summary>
    /// Returns a new Vector3Int based on input vector with modified Z value
    /// </summary>
    public static Vector3Int WithZ(this Vector3Int vector, int z) => new Vector3Int(vector.x, vector.y, z);

    /// <summary>
    /// Modifies the input Vector3Int with the given X value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetX(this ref Vector3Int vector, int x) => vector.x = x;

    /// <summary>
    /// Modifies the input Vector3Int with the given Y value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetY(this ref Vector3Int vector, int y) => vector.y = y;

    /// <summary>
    /// Modifies the input Vector3Int with the given Z value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetZ(this ref Vector3Int vector, int z) => vector.z = z;

    /// <summary>
    /// Converts a Vector3Int to a Vector2Int by ignoring the Z component.
    /// </summary>
    public static Vector2Int ConvertTo2D(this Vector3Int vector3) => new Vector2Int(vector3.x, vector3.y);
    public static Vector3 ConvertToVector3(this Vector3Int vecInt) => (Vector3)(vecInt);
}

public static class HexVector2
{
    public static Vector2 Multiply(this Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
    public static Vector2 Divide(this Vector2 a, Vector2 b) => new Vector2(a.x / b.x, a.y / b.y);
    public static Vector2 Abs(this Vector2 vector) => new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

    /// <summary>
    /// Rotates the given Vector2 by the given degree clockwise. Does not modify the original vector, returns a new one
    /// </summary>
    public static Vector2 WithRotation(this Vector2 vector, float degrees)
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
    /// Returns a new Vector2 based on input vector with modified X value
    /// </summary>
    public static Vector2 WithX(this Vector2 vector, float x) => new Vector2(x, vector.y);

    /// <summary>
    /// Returns a new Vector2 based on input vector with modified Y value
    /// </summary>
    public static Vector2 WithY(this Vector2 vector, float y) => new Vector2(vector.x, y);

    /// <summary>
    /// Modifies the input Vector2 with the given X value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetX(this ref Vector2 vector, float x) => vector.x = x;

    /// <summary>
    /// Modifies the input Vector2 with the given Y value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetY(this ref Vector2 vector, float y) => vector.y = y;

    /// <summary>
    /// Calculates the squared distance between two Vector2 instances.
    /// (a - b).sqrMagnitude
    /// </summary>
    public static float SqrDistance(this Vector2 a, Vector2 b) => (a - b).sqrMagnitude;

    /// <summary>
    /// Generates a random Vector2 with values between -1 and 1.
    /// </summary>
    public static Vector2 Random()
        => new Vector2(
                       UnityEngine.Random.value - 0.5f,
                       UnityEngine.Random.value - 0.5f
                      ) * 2;

    /// <summary>
    /// Generates a random Vector2 with values between 0 and 1.
    /// </summary>
    public static Vector2 Random01()
        => new Vector2(
                       UnityEngine.Random.value,
                       UnityEngine.Random.value
                       );

    /// <summary>
    /// Generates a random Vector2 between the provided min and max vectors.
    /// </summary>
    public static Vector2 RandomRange(Vector2 min, Vector2 max)
        => new Vector2(
                       UnityEngine.Random.Range(min.x, max.x),
                       UnityEngine.Random.Range(min.y, max.y)
                       );

    /// <summary>
    /// Determines if two Vector2 instances are nearly equal based on inaccuracy tolerance.
    /// </summary>
    public static bool NearlyEquals(this Vector2 a, Vector2 b, double inaccuracy = 9.0E-11) =>
        Vector2.SqrMagnitude(a - b) < inaccuracy;
}

public static class HexVector2Int
{
    public static Vector2Int Multiply(this Vector2Int a, Vector2Int b) => new Vector2Int(a.x * b.x, a.y * b.y);
    public static Vector2Int Divide(this Vector2Int a, Vector2Int b) => new Vector2Int(a.x / b.x, a.y / b.y);
    public static Vector2Int Abs(this Vector2Int vector) => new Vector2Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

    /// <summary>
    /// Returns a new Vector2Int based on input vector with modified X value
    /// </summary>
    public static Vector2Int WithX(this Vector2Int vector, int x) => new Vector2Int(x, vector.y);

    /// <summary>
    /// Returns a new Vector2Int based on input vector with modified Y value
    /// </summary>
    public static Vector2Int WithY(this Vector2Int vector, int y) => new Vector2Int(vector.x, y);

    /// <summary>
    /// Modifies the input Vector2Int with the given X value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetX(this ref Vector2Int vector, int x) => vector.x = x;

    /// <summary>
    /// Modifies the input Vector2Int with the given Y value.
    /// Vector is provided by reference.
    /// </summary>
    public static void SetY(this ref Vector2Int vector, int y) => vector.y = y;

    /// <summary>
    /// Calculates the squared distance between two Vector2Int instances.
    /// (a - b).sqrMagnitude
    /// </summary>
    public static float SqrDistance(this Vector2Int a, Vector2Int b) => (a - b).sqrMagnitude;
    public static Vector2 ConvertToVector2(this Vector2Int vecInt) => (Vector2)(vecInt);
}
