using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public static partial class HexTransform
{
    public static void Reset([DisallowNull] this Transform t)
    {
        t.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        t.localScale = Vector3.one;
    }
}