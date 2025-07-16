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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithRed(this Color color, float r) => new Color(r, color.g, color.b, color.a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithGreen(this Color color, float g) => new Color(color.r, g, color.b, color.a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithBlue(this Color color, float b) => new Color(color.r, color.g, b, color.a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithAlpha(this Color color, float a) => new Color(color.r, color.g, color.b, a);

    /// <summary>
    /// Applies brightness to a color. Brightness does not have to lie in the range of [0-1].
    /// </summary>
    public static Color WithBrightness(this Color color, float brightness)
    {
        return color.NormalizeRGB() * brightness;
    }

    public static float GetRGBMagnitude(this Color color)
    {
        return Mathf.Sqrt(color.r * color.r + color.g * color.g + color.b * color.b);
    }

    public static float GetRGBAMagnitude(this Color color)
    {
        return Mathf.Sqrt(color.r * color.r + color.g * color.g + color.b * color.b + color.a * color.a);
    }

    public static Color ScaleRGB(this Color color, float scalar)
    {
        return new Color(color.r * scalar, color.g * scalar, color.b * scalar, color.a);
    }

    public static Color ScaleRGBA(this Color color, float scalar)
    {
        return new Color(color.r * scalar, color.g * scalar, color.b * scalar, color.a * scalar);
    }

    /// <summary>
    /// Divides RGB color by its length like a vector. Results in a color such that it does not depend on the input brightness. Output brightness is always the same.
    /// </summary>
    public static Color NormalizeRGB(this Color color)
    {
        return ScaleRGB(color, 1.0f / color.GetRGBMagnitude());
    }

    /// <summary>
    /// Divides RGBA color by its length like a vector. Results in a color such that it does not depend on the input brightness. Output brightness is always the same. Output saturation differs from the input.
    /// </summary>
    public static Color NormalizeRGBA(this Color color)
    {
        return ScaleRGBA(color, 1.0f / color.GetRGBAMagnitude());
    }

    public static string WrapInHTMLTagRGB(this Color color, string @string)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{@string}</color>";
    }
    public static string WrapInHTMLTagRGBA(this Color color, string @string)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{@string}</color>";
    }
}