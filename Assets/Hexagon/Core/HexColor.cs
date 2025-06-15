using UnityEngine;
using System.Runtime.CompilerServices;

public static class ModHexColor
{
    /// <summary>
    /// Converts a Vector3 to a Color with the desired alpha. Default alpha is 1.0f.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color VectorToColor(this Vector3 vector, float a = 1.0f) => new Color(vector.x, vector.y, vector.z, a);

    /// <summary>
    /// Converts a Color to a Vector3 representation. The result Vector3 values are within 0.0 - 1.0 range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ColorToVector(this Color color) => new Vector3(color.r, color.g, color.b);
}