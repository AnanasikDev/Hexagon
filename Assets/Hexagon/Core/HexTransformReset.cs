using UnityEngine;

public static partial class HexTransform
{
    /// <summary>
    /// Resets position, rotation and scale of the transform to default values (all-zeros for position and rotation and all-ones for scale).
    /// </summary>
    /// <param name="transform"></param>
    public static void Reset(this Transform transform)
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.one;
    }
}