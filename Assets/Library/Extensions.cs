using UnityEngine;
using System;
public static class Extensions
{
    public static bool Multiples(this int a, int b)
    {
        return b != 0 && a % b == 0 && a != 0;
    }
    public static Color ToColor(this string color)
    {
        if (color.StartsWith("#"))
        {
            color = color.Substring(1); // strip #
        }

        if (color.Length == 6)
        {
            color += "FF"; // add alpha if missing
        }

        var hex = Convert.ToUInt32(color, 16);
        var r = ((hex & 0xff000000) >> 0x18) / 255f;
        var g = ((hex & 0xff0000) >> 0x10) / 255f;
        var b = ((hex & 0xff00) >> 8) / 255f;
        var a = ((hex & 0xff)) / 255f;

        return new Color(r, g, b, a);
    }
}