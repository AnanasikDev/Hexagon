using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public static partial class HexTransform
{
    public static void Reset([DisallowNull] this Transform transform)
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.one;
    }
}