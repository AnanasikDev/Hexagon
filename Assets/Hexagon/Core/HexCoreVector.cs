using UnityEngine;
using System;

public static class HexVector3
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
    /// <summary>
    /// Calls function with each pair of a and b elements and puts the result in the output vector
    /// </summary>
    /// <param name="a">Vector 1</param>
    /// <param name="b">Vector 2</param>
    /// <param name="function">Function to call; First arg: a.axis; Second arg: b.axis; Result: output.axis</param>
    public static Vector3 DoManual(this Vector3 a, Vector3 b, Func<float, float, float> function)
    {
        return new Vector3(function(a.x, b.x), function(a.y, b.y), function(a.z, b.z));
    }
    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }
    public static Vector3 NullZ(this Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }
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
    public static float SqrDistance2D(this Vector2 a, Vector2 b)
    {
        return (a - b).sqrMagnitude;
    }
    /// <summary>
    /// Calculates the distance between vector3D a and vector3D b in 2D space (ignoring Z-axis):
    /// (a - b).sqrMagnitude
    /// </summary>
    public static float SqrDistance2D(this Vector3 a, Vector3 b)
    {
        return (a - b).SqrMagnitudeXY();
    }
    public static float SqrMagnitudeXY(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y).sqrMagnitude;
    }
}

public static class HexVector3Int
{
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
    /// <summary>
    /// Calls function with each pair of a and b elements and puts the result in the output vector
    /// </summary>
    /// <param name="a">Vector 1</param>
    /// <param name="b">Vector 2</param>
    /// <param name="function">Function to call; First arg: a.axis; Second arg: b.axis; Result: output.axis</param>
    public static Vector3Int DoManual(this Vector3Int a, Vector3Int b, Func<int, int, int> function)
    {
        return new Vector3Int(function(a.x, b.x), function(a.y, b.y), function(a.z, b.z));
    }
    public static Vector3Int Abs(this Vector3Int vector)
    {
        return new Vector3Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }
    public static Vector3Int NullZ(this Vector3Int vector)
    {
        return new Vector3Int(vector.x, vector.y, 0);
    }
}

public static class HexVector2
{
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
    /// <summary>
    /// Calls function with each pair of a and b elements and puts the result in the output vector
    /// </summary>
    /// <param name="a">Vector 1</param>
    /// <param name="b">Vector 2</param>
    /// <param name="function">Function to call; First arg: a.axis; Second arg: b.axis; Result: output.axis</param>
    public static Vector2 DoManual(this Vector2 a, Vector2 b, Func<float, float, float> function)
    {
        return new Vector2(function(a.x, b.x), function(a.y, b.y));
    }
    public static Vector2 Abs(this Vector2 vector)
    {
        return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }
}

public static class HexVector2Int
{
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
    /// <summary>
    /// Calls function with each pair of a and b elements and puts the result in the output vector
    /// </summary>
    /// <param name="a">Vector 1</param>
    /// <param name="b">Vector 2</param>
    /// <param name="function">Function to call; First arg: a.axis; Second arg: b.axis; Result: output.axis</param>
    public static Vector2Int DoManual(this Vector2Int a, Vector2Int b, Func<int, int, int> function)
    {
        return new Vector2Int(function(a.x, b.x), function(a.y, b.y));
    }
    public static Vector2Int Abs(this Vector2Int vector)
    {
        return new Vector2Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }
}
