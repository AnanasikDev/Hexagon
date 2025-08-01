﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Class for general vector operations (multiplication, division, and absolute value) for all vector types.
/// </summary>
public static class HexVectorOps
{
    #region Scale

    /// <summary>
    /// Multiplies two Vector3 instances element-wise and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Scale(this Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);

    /// <summary>
    /// Multiplies two Vector3Int instances element-wise and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int Scale(this Vector3Int a, Vector3Int b) => new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);

    /// <summary>
    /// Multiplies two Vector2 instances element-wise and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Scale(this Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);

    /// <summary>
    /// Multiplies two Vector2Int instances element-wise and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int Scale(this Vector2Int a, Vector2Int b) => new Vector2Int(a.x * b.x, a.y * b.y);

    /// <summary>
    /// Scales only X component of the given vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ScaleX(this Vector3 vec, float factor) => new Vector3(vec.x * factor, vec.y, vec.z);

    /// <summary>
    /// Scales only Y component of the given vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ScaleY(this Vector3 vec, float factor) => new Vector3(vec.x, vec.y * factor, vec.z);

    /// <summary>
    /// Scales only Z component of the given vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ScaleZ(this Vector3 vec, float factor) => new Vector3(vec.x, vec.y, vec.z * factor);

    /// <summary>
    /// Scales only X component of the given vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ScaleX(this Vector2 vec, float factor) => new Vector3(vec.x * factor, vec.y);

    /// <summary>
    /// Scales only Y component of the given vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ScaleY(this Vector2 vec, float factor) => new Vector3(vec.x, vec.y * factor);

    /// <summary>
    /// Divides two Vector3 instances element-wise and returns the result.
    /// </summary>
    /// <remarks>Ensure the second vector does not have zero components to avoid division by zero.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Divide(this Vector3 a, Vector3 b) => new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);

    /// <summary>
    /// Divides two Vector3Int instances element-wise and returns the result.
    /// </summary>
    /// <remarks>Ensure the second vector does not have zero components to avoid division by zero.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int Divide(this Vector3Int a, Vector3Int b) => new Vector3Int(a.x / b.x, a.y / b.y, a.z / b.z);

    /// <summary>
    /// Divides two Vector2 instances element-wise and returns the result.
    /// </summary>
    /// <remarks>Ensure the second vector does not have zero components to avoid division by zero.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Divide(this Vector2 a, Vector2 b) => new Vector2(a.x / b.x, a.y / b.y);

    /// <summary>
    /// Divides two Vector2Int instances element-wise and returns the result.
    /// </summary>
    /// <remarks>Ensure the second vector does not have zero components to avoid division by zero.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int Divide(this Vector2Int a, Vector2Int b) => new Vector2Int(a.x / b.x, a.y / b.y);

    #endregion // scale

    #region Abs

    /// <summary>
    /// Returns a new Vector3 with the absolute values of the components.
    /// </summary>
    public static Vector3 Abs(this Vector3 vector) => new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));

    /// <summary>
    /// Returns a new Vector3Int with the absolute values of the components.
    /// </summary>
    public static Vector3Int Abs(this Vector3Int vector) => new Vector3Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));

    /// <summary>
    /// Returns a new Vector2 with the absolute values of the components.
    /// </summary>
    public static Vector2 Abs(this Vector2 vector) => new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

    /// <summary>
    /// Returns a new Vector2Int with the absolute values of the components.
    /// </summary>
    public static Vector2Int Abs(this Vector2Int vector) => new Vector2Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

    #endregion // abs

    #region Exponents

    /// <summary>
    /// Raises each component to the specified exponent
    /// </summary>
    public static Vector3 Pow(this Vector3 vector, float exponent) => new Vector3(Mathf.Pow(vector.x, exponent), Mathf.Pow(vector.y, exponent), Mathf.Pow(vector.z, exponent));

    /// <summary>
    /// Raises each component to the specified exponent
    /// </summary>
    public static Vector2 Pow(this Vector2 vector, float exponent) => new Vector2(Mathf.Pow(vector.x, exponent), Mathf.Pow(vector.y, exponent));

    /// <summary>
    /// Takes square root of each component
    /// </summary>
    public static Vector3 Sqrt(this Vector3 vector) => new Vector3(Mathf.Sqrt(vector.x), Mathf.Sqrt(vector.y), Mathf.Sqrt(vector.z));

    /// <summary>
    /// Takes square root of each component
    /// </summary>
    public static Vector2 Sqrt(this Vector2 vector) => new Vector2(Mathf.Sqrt(vector.x), Mathf.Sqrt(vector.y));

    #endregion // exponents

    #region Average

    /// <summary>
    /// Returns an average point of the given list of vectors.
    /// </summary>
    public static Vector2 Average(this IEnumerable<Vector2> vectors)
    {
        Assert.IsNotNull(vectors);
        Vector2 result = Vector2.zero;
        int count = 0;
        foreach (Vector2 iter in vectors)
        {
            result += iter;
            count++;
        }
        Assert.AreNotEqual(0, count);
        return result / count;
    }

    /// <summary>
    /// Returns an average point of the given list of vectors.
    /// </summary>
    public static Vector3 Average(this IEnumerable<Vector3> vectors)
    {
        Assert.IsNotNull(vectors);
        Vector3 result = Vector3.zero;
        int count = 0;
        foreach (Vector3 iter in vectors)
        {
            result += iter;
            count++;
        }
        Assert.AreNotEqual(0, count);
        return result / count;
    }

    #endregion // average
}

/// <summary>
/// Class for axis operations for all types of vectors.
/// </summary>
public static class HexVectorAxisOps
{
    #region With

    /// <summary>
    /// Returns a copy of the given Vector3 with the Z component set to 0.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 NullZ(this Vector3 vector) => 
        new Vector3(vector.x, vector.y, 0);

    /// <summary>
    /// Creates a new Vector3 with the specified X component, retaining the original Y and Z components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithX(this Vector3 vector, float x) => 
        new Vector3(x, vector.y, vector.z);

    /// <summary>
    /// Creates a new Vector3 with the specified Y component, retaining the original X and Z components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithY(this Vector3 vector, float y) => 
        new Vector3(vector.x, y, vector.z);

    /// <summary>
    /// Creates a new Vector3 with the specified Z component, retaining the original X and Y components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithZ(this Vector3 vector, float z) => 
        new Vector3(vector.x, vector.y, z);

    /// <summary>
    /// Creates a new Vector3 with the specified components, leaving the unspecified ones as they were.
    /// </summary>
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) => 
        new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);

    /// <summary>
    /// Creates a new Vector3Int with the specified X component, retaining the original Y and Z components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithX(this Vector3Int vector, int x) => 
        new Vector3Int(x, vector.y, vector.z);

    /// <summary>
    /// Creates a new Vector3Int with the specified Y component, retaining the original X and Z components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithY(this Vector3Int vector, int y) => 
        new Vector3Int(vector.x, y, vector.z);

    /// <summary>
    /// Creates a new Vector3Int with the specified Z component, retaining the original X and Y components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithZ(this Vector3Int vector, int z) => 
        new Vector3Int(vector.x, vector.y, z);

    /// <summary>
    /// Creates a new Vector3 with the specified components, leaving the unspecified ones as they were.
    /// </summary>
    public static Vector3Int With(this Vector3Int vector, int? x = null, int? y = null, int? z = null) => 
        new Vector3Int(x ?? vector.x, y ?? vector.y, z ?? vector.z);

    /// <summary>
    /// Creates a new Vector2 with the specified X component, retaining the original Y component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 WithX(this Vector2 vector, float x) => 
        new Vector2(x, vector.y);

    /// <summary>
    /// Creates a new Vector2 with the specified Y component, retaining the original X component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 WithY(this Vector2 vector, float y) => 
        new Vector2(vector.x, y);

    /// <summary>
    /// Creates a new Vector3 with the specified Z component, retaining the original X and Y components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithZ3D(this Vector2 vector, float z) => 
        new Vector3(vector.x, vector.y, z);

    /// <summary>
    /// Creates a new Vector3 with the specified components, leaving the unspecified ones as they were.
    /// </summary>
    public static Vector2 With(this Vector2 vector, float? x = null, float? y = null) => 
        new Vector2(x ?? vector.x, y ?? vector.y);

    /// <summary>
    /// Creates a new Vector3 with the specified components, leaving the unspecified ones as they were.
    /// </summary>
    public static Vector3 With3D(this Vector2 vector, float? x = null, float? y = null, float z = 0) => 
        new Vector3(x ?? vector.x, y ?? vector.y, z);

    /// <summary>
    /// Creates a new Vector2Int with the specified X component, retaining the original Y component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int WithX(this Vector2Int vector, int x) => 
        new Vector2Int(x, vector.y);

    /// <summary>
    /// Creates a new Vector2Int with the specified Y component, retaining the original X component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int WithY(this Vector2Int vector, int y) => 
        new Vector2Int(vector.x, y);

    /// <summary>
    /// Creates a new Vector3Int with the specified Y component, retaining the original X component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithZ3D(this Vector2Int vector, int z) => 
        new Vector3Int(vector.x, vector.y, z);

    /// <summary>
    /// Creates a new Vector3 with the specified components, leaving the unspecified ones as they were.
    /// </summary>
    public static Vector2Int With(this Vector2Int vector, int? x = null, int? y = null) => 
        new Vector2Int(x ?? vector.x, y ?? vector.y);

    #endregion // with

    #region Set

    /// <summary>
    /// Sets the X component of the Vector3.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetX(this ref Vector3 vector, float x) => vector.x = x;

    /// <summary>
    /// Sets the Y component of the Vector3.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetY(this ref Vector3 vector, float y) => vector.y = y;

    /// <summary>
    /// Sets the Z component of the Vector3.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetZ(this ref Vector3 vector, float z) => vector.z = z;

    /// <summary>
    /// Sets the specified components of the Vector3, leaving the unspecified ones as they were.
    /// </summary>
    public static void Set(this ref Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        vector.x = x ?? vector.x;
        vector.y = y ?? vector.y;
        vector.z = z ?? vector.z;
    }

    /// <summary>
    /// Sets the X component of the Vector3Int.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetX(this ref Vector3Int vector, int x) => vector.x = x;

    /// <summary>
    /// Sets the Y component of the Vector3Int.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetY(this ref Vector3Int vector, int y) => vector.y = y;

    /// <summary>
    /// Sets the Z component of the Vector3Int.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetZ(this ref Vector3Int vector, int z) => vector.z = z;

    /// <summary>
    /// Sets the specified components of the Vector3Int, leaving the unspecified ones as they were.
    /// </summary>
    public static void Set(this ref Vector3Int vector, int? x = null, int? y = null, int? z = null)
    {
        vector.x = x ?? vector.x;
        vector.y = y ?? vector.y;
        vector.z = z ?? vector.z;
    }

    /// <summary>
    /// Sets the X component of the Vector2.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetX(this ref Vector2 vector, float x) => vector.x = x;

    /// <summary>
    /// Sets the Y component of the Vector2.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetY(this ref Vector2 vector, float y) => vector.y = y;

    /// <summary>
    /// Sets the specified components of the Vector2, leaving the unspecified ones as they were.
    /// </summary>
    public static void Set(this ref Vector2 vector, float? x = null, float? y = null)
    {
        vector.x = x ?? vector.x;
        vector.y = y ?? vector.y;
    }

    /// <summary>
    /// Sets the X component of the Vector2Int.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetX(this ref Vector2Int vector, int x) => vector.x = x;

    /// <summary>
    /// Sets the Y component of the Vector2Int.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetY(this ref Vector2Int vector, int y) => vector.y = y;

    /// <summary>
    /// Sets the specified components of the Vector2, leaving the unspecified ones as they were.
    /// </summary>
    public static void Set(this ref Vector2Int vector, int? x = null, int? y = null)
    {
        vector.x = x ?? vector.x;
        vector.y = y ?? vector.y;
    }

    #endregion // set
}

/// <summary>
/// Class for utility operations involving vectors:
/// <list type="bullet">
/// <item> Conversion operations </item>
/// <item> Distance functions </item>
/// <item> Nearly equals functions </item>
/// </list>
/// </summary>
public static class HexVectorUtils
{
    #region Convert

    /// <summary>
    /// Converts a Vector3 to a Vector2 by ignoring the Z component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ConvertTo2D(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);

    /// <summary>
    /// Converts a Vector2 to a Vector3 with Z set to 0.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ConvertTo3D(this Vector2 vector2) => new Vector3(vector2.x, vector2.y, 0);

    /// <summary>
    /// Converts a Vector2Int to a Vector2.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ConvertTo2D(this Vector2Int vector2Int) => new Vector2(vector2Int.x, vector2Int.y);

    /// <summary>
    /// Converts a Vector3Int to a Vector3.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ConvertTo3D(this Vector3Int vector3Int) => new Vector3(vector3Int.x, vector3Int.y, vector3Int.z);

    #endregion // convert

    #region To int

    /// <summary>
    /// Rounds the components of a Vector2 to the nearest integer values and returns a Vector2Int.
    /// </summary>
    /// <returns>A new Vector2Int with rounded integer values.</returns>
    /// <remarks>
    /// This method uses Mathf.RoundToInt.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int RoundToInt(this Vector2 vector2)
    {
        return new Vector2Int(
            Mathf.RoundToInt(vector2.x),
            Mathf.RoundToInt(vector2.y)
        );
    }

    /// <summary>
    /// Rounds the components of a Vector3 to the nearest integer values and returns a Vector3Int.
    /// </summary>
    /// <returns>A new Vector3Int with rounded integer values.</returns>
    /// <remarks>
    /// This method uses Mathf.RoundToInt.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int RoundToInt(this Vector3 vector3)
    {
        return new Vector3Int(
            Mathf.RoundToInt(vector3.x),
            Mathf.RoundToInt(vector3.y),
            Mathf.RoundToInt(vector3.z)
        );
    }

    /// <summary>
    /// Ceils the components of a Vector2 and returns a Vector2Int.
    /// </summary>
    /// <returns>A new Vector2Int with ceiled integer values.</returns>
    /// <remarks>
    /// This method uses Mathf.CeilToInt.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int CeilToInt(this Vector2 vector2)
    {
        return new Vector2Int(
            Mathf.CeilToInt(vector2.x),
            Mathf.CeilToInt(vector2.y)
        );
    }

    /// <summary>
    /// Ceils the components of a Vector3 and returns a Vector3Int.
    /// </summary>
    /// <returns>A new Vector3Int with ceiled integer values.</returns>
    /// <remarks>
    /// This method uses Mathf.CeilToInt.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int CeilToInt(this Vector3 vector3)
    {
        return new Vector3Int(
            Mathf.CeilToInt(vector3.x),
            Mathf.CeilToInt(vector3.y),
            Mathf.CeilToInt(vector3.z)
        );
    }

    /// <summary>
    /// Floors the components of a Vector2 and returns a Vector2Int.
    /// </summary>
    /// <returns>A new Vector2Int with floored integer values.</returns>
    /// <remarks>
    /// This method uses Mathf.FloorToInt.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int FloorToInt(this Vector2 vector2)
    {
        return new Vector2Int(
            Mathf.FloorToInt(vector2.x),
            Mathf.FloorToInt(vector2.y)
        );
    }

    /// <summary>
    /// Floors the components of a Vector3 and returns a Vector3Int.
    /// </summary>
    /// <returns>A new Vector3Int with floored integer values.</returns>
    /// <remarks>
    /// This method uses Mathf.FloorToInt.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int FloorToInt(this Vector3 vector3)
    {
        return new Vector3Int(
            Mathf.FloorToInt(vector3.x),
            Mathf.FloorToInt(vector3.y),
            Mathf.FloorToInt(vector3.z)
        );
    }

    #endregion // to int

    #region Distance

    /// <summary>
    /// Calculates the squared distance between two Vector2.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SqrDistance(this Vector2 a, Vector2 b) => (a - b).sqrMagnitude;

    /// <summary>
    /// Calculates the squared distance between two Vector2Int.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SqrDistance(this Vector2Int a, Vector2Int b) => (a - b).sqrMagnitude;

    /// <summary>
    /// Calculates the squared distance between two Vector3.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SqrDistance(this Vector3 a, Vector3 b) => (a - b).sqrMagnitude;

    /// <summary>
    /// Calculates the squared distance between two Vector3Int.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SqrDistance(this Vector3Int a, Vector3Int b) => (a - b).sqrMagnitude;

    /// <summary>
    /// Calculates the squared distance between two Vector3 instances in 2D space (in XY-plane).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SqrDistanceXY(this Vector3 a, Vector3 b) => (a - b).NullZ().sqrMagnitude;

    /// <summary>
    /// Calculates the distance between two Vector2.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Distance(this Vector2 a, Vector2 b) => Vector2.Distance(a, b);

    /// <summary>
    /// Calculates the distance between two Vector2Int.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Distance(this Vector2Int a, Vector2Int b) => Vector2Int.Distance(a, b);

    /// <summary>
    /// Calculates the distance between two Vector3.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Distance(this Vector3 a, Vector3 b) => Vector3.Distance(a, b);

    /// <summary>
    /// Calculates the distance between two Vector3Int.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Distance(this Vector3Int a, Vector3Int b) => Vector3Int.Distance(a, b);

    /// <summary>
    /// Calculates the distance between two Vector3 instances in 2D space (in XY-plane).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float DistanceXY(this Vector3 a, Vector3 b) => (a - b).NullZ().magnitude;

    #endregion // distance

    #region Nearly equals

    /// <summary>
    /// Checks if two Vector3 instances are nearly equal based on an inaccuracy tolerance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEquals(this Vector3 a, Vector3 b, double inaccuracy = 1.0E-5) => Vector3.SqrMagnitude(a - b) < inaccuracy;

    /// <summary>
    /// Checks if two Vector3 instances are nearly equal based on an inaccuracy tolerance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEquals(this Vector2 a, Vector2 b, double inaccuracy = 1.0E-5) => Vector2.SqrMagnitude(a - b) < inaccuracy;

    #endregion // nearly equals
}

public static class HexVectorGeom
{
    #region Rotate

    /// <summary>
    /// Rotates the given Vector2 by the given degree.
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

    public static Vector3 Rotate(this Vector3 vector, Vector3 planeNormal, float degrees)
    {
        return Quaternion.AngleAxis(degrees, planeNormal) * vector;
    }

    public static Vector2 RotateAround2D(this Vector2 point, Vector2 pivotPoint, float degrees)
    {
        Quaternion rotation = Quaternion.AngleAxis(degrees, new Vector3(0, 0, 1));
        Vector3 diff = point - pivotPoint;
        Vector3 result = pivotPoint.ConvertTo3D() + (rotation * diff);
        return result;
    }

    public static Vector3 RotateAround3D(this Vector3 point, Vector3 pivotPoint, Vector3 axis, float degrees)
    {
        Quaternion rotation = Quaternion.AngleAxis(degrees, axis);
        Vector3 diff = point - pivotPoint;
        Vector3 result = pivotPoint + (rotation * diff);
        return result;
    }

    #endregion // rotate
}

public static class HexVectorMath
{
    #region Clamp

    /// <summary>
    /// Clamps the given Vector2 to 0.0 - 1.0 range
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Clamp01(this Vector2 vector) =>
        new Vector2(Mathf.Clamp01(vector.x), Mathf.Clamp01(vector.y));

    /// <summary>
    /// Clamps the given Vector3 to 0.0 - 1.0 range
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Clamp01(this Vector3 vector) =>
        new Vector3(Mathf.Clamp01(vector.x), Mathf.Clamp01(vector.y), Mathf.Clamp01(vector.z));

    #endregion // clamp

    #region Sum

    /// <summary>
    /// Returns the sum of all components of the input vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sum(this Vector2 v) => v.x + v.y;

    /// <summary>
    /// Returns the sum of all components of the input vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sum(this Vector3 v) => v.x + v.y + v.z;

    /// <summary>
    /// Returns the sum of all components of the input vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sum(this Vector4 v) => v.x + v.y + v.z + v.w;

    /// <summary>
    /// Returns the sum of all components of the input vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sum(this Vector2Int v) => v.x + v.y;

    /// <summary>
    /// Returns the sum of all components of the input vector
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sum(this Vector3Int v) => v.x + v.y + v.z;

    #endregion // sum

    public static Vector2 SnapToStep(this in Vector2 vec, float step)
    {
        return new Vector2(Hexath.SnapNumberToStep(vec.x, step), Hexath.SnapNumberToStep(vec.y, step));
    }

    public static Vector3 SnapToStep(this in Vector3 vec, float step)
    {
        return new Vector3(Hexath.SnapNumberToStep(vec.x, step), Hexath.SnapNumberToStep(vec.y, step), Hexath.SnapNumberToStep(vec.z, step));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Remainder(this in Vector2 vec, float value)
    {
        return new Vector2(vec.x % value, vec.y % value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Remainder(this in Vector3 vec, float value)
    {
        return new Vector3(vec.x % value, vec.y % value, vec.z % value);
    }
}