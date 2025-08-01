using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Provides extension methods for Unity Color and Vector3 types to simplify common color transformations and formatting.
/// </summary>
public static class HexColor
{
    /// <summary>
    /// Converts a Vector3 to a Color, using optional alpha.
    /// </summary>
    /// <param name="vector">Vector3 with RGB components in [0,1] range.</param>
    /// <param name="a">Alpha value to use, default is 1.0.</param>
    /// <returns>A Color constructed from the vector and alpha.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color VectorToColor(this Vector3 vector, float a = 1.0f) => new Color(vector.x, vector.y, vector.z, a);

    /// <summary>
    /// Converts a Vector4 to a Color.
    /// </summary>
    /// <param name="vector">Vector4 with RGBA components in [0,1] range.</param>
    /// <returns>A Color constructed from the vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color VectorToColor(this Vector4 vector) => new Color(vector.x, vector.y, vector.z, vector.w);

    /// <summary>
    /// Converts a Color to a Vector3 containing only RGB components.
    /// </summary>
    /// <param name="color">Input Color.</param>
    /// <returns>Vector3 with r, g, b components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ColorToVector3(this Color color) => new Vector3(color.r, color.g, color.b);

    /// <summary>
    /// Converts a Color to a Vector4.
    /// </summary>
    /// <param name="color">Input Color.</param>
    /// <returns>Vector4 with r, g, b, a components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 ColorToVector4(this Color color) => new Vector4(color.r, color.g, color.b, color.a);

    /// <summary>
    /// Returns a copy of the color with a new red component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithRed(this Color color, float r) => new Color(r, color.g, color.b, color.a);

    /// <summary>
    /// Returns a copy of the color with a new green component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithGreen(this Color color, float g) => new Color(color.r, g, color.b, color.a);

    /// <summary>
    /// Returns a copy of the color with a new blue component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithBlue(this Color color, float b) => new Color(color.r, color.g, b, color.a);

    /// <summary>
    /// Returns a copy of the color with a new alpha component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithAlpha(this Color color, float a) => new Color(color.r, color.g, color.b, a);

    /// <summary>
    /// Applies brightness scaling after normalizing RGB components.
    /// </summary>
    /// <param name="color">Input color to adjust.</param>
    /// <param name="brightness">Scaling factor for brightness.</param>
    /// <returns>Normalized and brightness-scaled color.</returns>
    public static Color WithBrightness(this Color color, float brightness)
    {
        return color.NormalizeRGB() * brightness;
    }

    /// <summary>
    /// Computes Euclidean length of RGB components.
    /// </summary>
    /// <param name="color">Input color.</param>
    /// <returns>Magnitude of RGB vector.</returns>
    public static float GetRGBMagnitude(this Color color)
    {
        return Mathf.Sqrt(color.r * color.r + color.g * color.g + color.b * color.b);
    }

    /// <summary>
    /// Computes Euclidean length of RGBA components.
    /// </summary>
    /// <param name="color">Input color.</param>
    /// <returns>Magnitude of RGBA vector.</returns>
    public static float GetRGBAMagnitude(this Color color)
    {
        return Mathf.Sqrt(color.r * color.r + color.g * color.g + color.b * color.b + color.a * color.a);
    }

    /// <summary>
    /// Scales RGB channels by a given factor.
    /// </summary>
    /// <param name="color">Input color.</param>
    /// <param name="scalar">Scaling factor.</param>
    /// <returns>Color with scaled RGB, original alpha.</returns>
    public static Color ScaleRGB(this Color color, float scalar)
    {
        return new Color(color.r * scalar, color.g * scalar, color.b * scalar, color.a);
    }

    /// <summary>
    /// Scales all RGBA channels by a given factor.
    /// </summary>
    /// <param name="color">Input color.</param>
    /// <param name="scalar">Scaling factor.</param>
    /// <returns>Color with scaled RGBA components.</returns>
    public static Color ScaleRGBA(this Color color, float scalar)
    {
        return new Color(color.r * scalar, color.g * scalar, color.b * scalar, color.a * scalar);
    }

    /// <summary>
    /// Normalizes RGB components to unit length; output brightness is constant.
    /// </summary>
    /// <param name="color">Input color.</param>
    /// <returns>Color with normalized RGB and original alpha.</returns>
    public static Color NormalizeRGB(this Color color)
    {
        return ScaleRGB(color, 1.0f / color.GetRGBMagnitude());
    }

    /// <summary>
    /// Normalizes all RGBA components to unit length; output brightness is constant.
    /// </summary>
    /// <param name="color">Input color.</param>
    /// <returns>Color with normalized RGBA.</returns>
    public static Color NormalizeRGBA(this Color color)
    {
        return ScaleRGBA(color, 1.0f / color.GetRGBAMagnitude());
    }

    /// <summary>
    /// Wraps a string in a Unity color tag using RGB only.
    /// </summary>
    /// <param name="color">Text color.</param>
    /// <param name="string">String to wrap.</param>
    /// <returns>String with Unity color tag applied.</returns>
    public static string WrapInHTMLTagRGB(this Color color, string @string)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{@string}</color>";
    }

    /// <summary>
    /// Wraps a string in a Unity color tag including alpha.
    /// </summary>
    /// <param name="color">Text color.</param>
    /// <param name="string">String to wrap.</param>
    /// <returns>String with Unity color tag applied.</returns>
    public static string WrapInHTMLTagRGBA(this Color color, string @string)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{@string}</color>";
    }
}
