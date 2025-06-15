using System.Runtime.CompilerServices;
using UnityEngine;

public static class Vector2Swizzles
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 xx(this Vector2 a) => new(a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 yx(this Vector2 a) => new(a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 xy(this Vector2 a) => new(a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 yy(this Vector2 a) => new(a.y, a.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xxx(this Vector2 a) => new(a.x, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yxx(this Vector2 a) => new(a.y, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xyx(this Vector2 a) => new(a.x, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yyx(this Vector2 a) => new(a.y, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xxy(this Vector2 a) => new(a.x, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yxy(this Vector2 a) => new(a.y, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xyy(this Vector2 a) => new(a.x, a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yyy(this Vector2 a) => new(a.y, a.y, a.y);
}

public static class Vector3Swizzles
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 xx(this Vector3 a) => new(a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 yx(this Vector3 a) => new(a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 zx(this Vector3 a) => new(a.z, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 xy(this Vector3 a) => new(a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 yy(this Vector3 a) => new(a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 zy(this Vector3 a) => new(a.z, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 xz(this Vector3 a) => new(a.x, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 yz(this Vector3 a) => new(a.y, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 zz(this Vector3 a) => new(a.z, a.z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xxx(this Vector3 a) => new(a.x, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yxx(this Vector3 a) => new(a.y, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 zxx(this Vector3 a) => new(a.z, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xyx(this Vector3 a) => new(a.x, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yyx(this Vector3 a) => new(a.y, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 zyx(this Vector3 a) => new(a.z, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xzx(this Vector3 a) => new(a.x, a.z, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yzx(this Vector3 a) => new(a.y, a.z, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 zzx(this Vector3 a) => new(a.z, a.z, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xxy(this Vector3 a) => new(a.x, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yxy(this Vector3 a) => new(a.y, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 zxy(this Vector3 a) => new(a.z, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xyy(this Vector3 a) => new(a.x, a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yyy(this Vector3 a) => new(a.y, a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 zyy(this Vector3 a) => new(a.z, a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xzy(this Vector3 a) => new(a.x, a.z, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yzy(this Vector3 a) => new(a.y, a.z, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 zzy(this Vector3 a) => new(a.z, a.z, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xxz(this Vector3 a) => new(a.x, a.x, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yxz(this Vector3 a) => new(a.y, a.x, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 zxz(this Vector3 a) => new(a.z, a.x, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xyz(this Vector3 a) => new(a.x, a.y, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yyz(this Vector3 a) => new(a.y, a.y, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 zyz(this Vector3 a) => new(a.z, a.y, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 xzz(this Vector3 a) => new(a.x, a.z, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 yzz(this Vector3 a) => new(a.y, a.z, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 zzz(this Vector3 a) => new(a.z, a.z, a.z);
}

// =================== //
// INT VECTOR SWIZZLES //
// =================== //

public static class Vector2IntSwizzles
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int xx(this Vector2Int a) => new(a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int yx(this Vector2Int a) => new(a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int xy(this Vector2Int a) => new(a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int yy(this Vector2Int a) => new(a.y, a.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xxx(this Vector2Int a) => new(a.x, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yxx(this Vector2Int a) => new(a.y, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xyx(this Vector2Int a) => new(a.x, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yyx(this Vector2Int a) => new(a.y, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xxy(this Vector2Int a) => new(a.x, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yxy(this Vector2Int a) => new(a.y, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xyy(this Vector2Int a) => new(a.x, a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yyy(this Vector2Int a) => new(a.y, a.y, a.y);
}

public static class Vector3IntSwizzles
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int xx(this Vector3Int a) => new(a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int yx(this Vector3Int a) => new(a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int zx(this Vector3Int a) => new(a.z, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int xy(this Vector3Int a) => new(a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int yy(this Vector3Int a) => new(a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int zy(this Vector3Int a) => new(a.z, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int xz(this Vector3Int a) => new(a.x, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int yz(this Vector3Int a) => new(a.y, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int zz(this Vector3Int a) => new(a.z, a.z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xxx(this Vector3Int a) => new(a.x, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yxx(this Vector3Int a) => new(a.y, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int zxx(this Vector3Int a) => new(a.z, a.x, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xyx(this Vector3Int a) => new(a.x, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yyx(this Vector3Int a) => new(a.y, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int zyx(this Vector3Int a) => new(a.z, a.y, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xzx(this Vector3Int a) => new(a.x, a.z, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yzx(this Vector3Int a) => new(a.y, a.z, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int zzx(this Vector3Int a) => new(a.z, a.z, a.x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xxy(this Vector3Int a) => new(a.x, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yxy(this Vector3Int a) => new(a.y, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int zxy(this Vector3Int a) => new(a.z, a.x, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xyy(this Vector3Int a) => new(a.x, a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yyy(this Vector3Int a) => new(a.y, a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int zyy(this Vector3Int a) => new(a.z, a.y, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xzy(this Vector3Int a) => new(a.x, a.z, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yzy(this Vector3Int a) => new(a.y, a.z, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int zzy(this Vector3Int a) => new(a.z, a.z, a.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xxz(this Vector3Int a) => new(a.x, a.x, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yxz(this Vector3Int a) => new(a.y, a.x, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int zxz(this Vector3Int a) => new(a.z, a.x, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xyz(this Vector3Int a) => new(a.x, a.y, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yyz(this Vector3Int a) => new(a.y, a.y, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int zyz(this Vector3Int a) => new(a.z, a.y, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int xzz(this Vector3Int a) => new(a.x, a.z, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int yzz(this Vector3Int a) => new(a.y, a.z, a.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3Int zzz(this Vector3Int a) => new(a.z, a.z, a.z);
}
