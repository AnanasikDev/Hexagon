# HexVector Documentation

## HexVectorOps
<details>
<summary>HexVectorOps Functions</summary>

### Overview
These functions perform element-wise arithmetic operations on vectors - multiplication, division, and taking absolute values. They work on `Vector3`, `Vector3Int`, `Vector2`, and `Vector2Int` types.

### Multiply (Element-wise)
```csharp
public static Vector3 Multiply(this Vector3 a, Vector3 b);
public static Vector3Int Multiply(this Vector3Int a, Vector3Int b);
public static Vector2 Multiply(this Vector2 a, Vector2 b);
public static Vector2Int Multiply(this Vector2Int a, Vector2Int b);
```
Multiplies each component of the first vector by the corresponding component of the second vector, returning a new vector with the result.

### Divide (Element-wise)
```csharp
public static Vector3 Divide(this Vector3 a, Vector3 b);
public static Vector3Int Divide(this Vector3Int a, Vector3Int b);
public static Vector2 Divide(this Vector2 a, Vector2 b);
public static Vector2Int Divide(this Vector2Int a, Vector2Int b);
```
Divides each component of the first vector by the corresponding component of the second vector, returning a new vector with the result.  
> **Remarks:** Watch out for division by zero in any vector component. Always check the second vector before dividing.

### Abs (Per component)
```csharp
public static Vector3 Abs(this Vector3 vector);
public static Vector3Int Abs(this Vector3Int vector);
public static Vector2 Abs(this Vector2 vector);
public static Vector2Int Abs(this Vector2Int vector);
```
Returns a new vector where each component is the absolute value of the corresponding component in the original vector.

</details>

## HexVectorAxisOps
<details>
<summary>HexVectorAxisOps Functions</summary>

### Overview
These functions allow inline manipulation of individual components of vectors. They provide a way to set values for individual axes (X, Y, Z).

### Set Axis Value
```csharp
public static void SetX(this ref Vector3 vector, float x);
public static void SetY(this ref Vector3 vector, float y);
public static void SetZ(this ref Vector3 vector, float z);
public static void SetX(this ref Vector3Int vector, int x);
public static void SetY(this ref Vector3Int vector, int y);
public static void SetZ(this ref Vector3Int vector, int z);
public static void SetX(this ref Vector2 vector, float x);
public static void SetY(this ref Vector2 vector, float y);
public static void SetX(this ref Vector2Int vector, int x);
public static void SetY(this ref Vector2Int vector, int y);
```
Directly modify a specific component (X, Y, or Z) of the given vector.

### With Axis Value
```csharp
public static Vector3 WithX(this Vector3 vector, float x);
public static Vector3 WithY(this Vector3 vector, float y);
public static Vector3 WithZ(this Vector3 vector, float z);
public static Vector3Int WithX(this Vector3Int vector, int x);
public static Vector3Int WithY(this Vector3Int vector, int y);
public static Vector3Int WithZ(this Vector3Int vector, int z);
public static Vector2 WithX(this Vector2 vector, float x);
public static Vector2 WithY(this Vector2 vector, float y);
public static Vector2Int WithX(this Vector2Int vector, int x);
public static Vector2Int WithY(this Vector2Int vector, int y);
```
Create a new vector where the specified component is replaced with the given value, keeping the original vector object unchanged.

### Null Z
```csharp
public static Vector3 NullZ(this Vector3 vector);
```
A shortcut for ```WithZ(0)```

</details>

## HexVectorRandomOps
<details>
<summary>HexVectorRandomOps Functions</summary>

### Random Vectors
```csharp
public static Vector3 Random3D();
public static Vector2 Random2D();
```
Generates a random vector where each component is a random float value between -1 and 1.

</details>

## HexVectorUtils
<details>
<summary>HexVectorUtils Functions</summary>

### Conversion
```csharp
public static Color VectorToColor(this Vector3 vector, float a = 1.0f);
public static Vector3 ColorToVector(this Color color);
public static Vector2 ConvertTo2D(this Vector3 vector3);
public static Vector2 ConvertTo2D(this Vector2Int vector2Int);
public static Vector3 ConvertTo3D(this Vector2 vector2);
public static Vector3 ConvertTo3D(this Vector3Int vector3Int);
```
- `VectorToColor`: Converts a `Vector3` to a `Color`, mapping the vector's components to RGB channels, and optionally setting the alpha channel.
- `ColorToVector`: Converts a `Color` back to a `Vector3`, using the RGB values.

### Distance Calculation
```csharp
public static float SqrDistance(this Vector2 a, Vector2 b);
public static float SqrDistance(this Vector2Int a, Vector2Int b);
public static float SqrDistance(this Vector3 a, Vector3 b);
public static float SqrDistanceXY(this Vector3 a, Vector3 b);
```
Calculates the squared distance between two vectors. `SqrDistanceXY` calculates squared distance in 2D space, ignoring the Z component.

### Comparison
```csharp
public static bool NearlyEquals(this Vector3 a, Vector3 b, double inaccuracy = 1.0E-7);
public static bool NearlyEquals(this Vector2 a, Vector2 b, double inaccuracy = 1.0E-7);
```
Checks if two vectors are nearly equal, allowing for a small tolerance (inaccuracy) to account for floating-point precision errors. Useful when comparing results of floating-point calculations.

</details>