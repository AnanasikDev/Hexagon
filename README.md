# HexVector Documentation

## HexVectorOps

<details>
<summary>HexVectorOps Functions</summary>

### Multiply
```csharp
public static Vector3 Multiply(this Vector3 a, Vector3 b);
```
Multiplies two Vector3 instances element-wise and returns the result.

### Multiply
```csharp
public static Vector3Int Multiply(this Vector3Int a, Vector3Int b);
```
Multiplies two Vector3Int instances element-wise and returns the result.

### Multiply
```csharp
public static Vector2 Multiply(this Vector2 a, Vector2 b);
```
Multiplies two Vector2 instances element-wise and returns the result.

### Multiply
```csharp
public static Vector2Int Multiply(this Vector2Int a, Vector2Int b);
```
Multiplies two Vector2Int instances element-wise and returns the result.

### Divide
```csharp
public static Vector3 Divide(this Vector3 a, Vector3 b);
```
Divides two Vector3 instances element-wise and returns the result.
> **Remarks:** Ensure the second vector does not have zero components to avoid division by zero.

### Divide
```csharp
public static Vector3Int Divide(this Vector3Int a, Vector3Int b);
```
Divides two Vector3Int instances element-wise and returns the result.
> **Remarks:** Ensure the second vector does not have zero components to avoid division by zero.

### Divide
```csharp
public static Vector2 Divide(this Vector2 a, Vector2 b);
```
Divides two Vector2 instances element-wise and returns the result.
> **Remarks:** Ensure the second vector does not have zero components to avoid division by zero.

### Divide
```csharp
public static Vector2Int Divide(this Vector2Int a, Vector2Int b);
```
Divides two Vector2Int instances element-wise and returns the result.
> **Remarks:** Ensure the second vector does not have zero components to avoid division by zero.

### Abs
```csharp
public static Vector3 Abs(this Vector3 vector);
```
Returns a new Vector3 with the absolute values of the components.

### Abs
```csharp
public static Vector3Int Abs(this Vector3Int vector);
```
Returns a new Vector3Int with the absolute values of the components.

### Abs
```csharp
public static Vector2 Abs(this Vector2 vector);
```
Returns a new Vector2 with the absolute values of the components.

### Abs
```csharp
public static Vector2Int Abs(this Vector2Int vector);
```
Returns a new Vector2Int with the absolute values of the components.

</details>

## HexVectorAxisOps

<details>
<summary>HexVectorAxisOps Functions</summary>

### NullZ
```csharp
public static Vector3 NullZ(this Vector3 vector);
```
Returns a copy of the given Vector3 with the Z component set to 0.

### WithX
```csharp
public static Vector3 WithX(this Vector3 vector, float x);
```
Creates a new Vector3 with the specified X component, retaining the original Y and Z components.

### WithY
```csharp
public static Vector3 WithY(this Vector3 vector, float y);
```
Creates a new Vector3 with the specified Y component, retaining the original X and Z components.

### WithZ
```csharp
public static Vector3 WithZ(this Vector3 vector, float z);
```
Creates a new Vector3 with the specified Z component, retaining the original X and Y components.

### SetX
```csharp
public static void SetX(this ref Vector3 vector, float x);
```
Sets the X component of the Vector3.

### SetY
```csharp
public static void SetY(this ref Vector3 vector, float y);
```
Sets the Y component of the Vector3.

### SetZ
```csharp
public static void SetZ(this ref Vector3 vector, float z);
```
Sets the Z component of the Vector3.

### WithX
```csharp
public static Vector3Int WithX(this Vector3Int vector, int x);
```
Creates a new Vector3Int with the specified X component, retaining the original Y and Z components.

### WithY
```csharp
public static Vector3Int WithY(this Vector3Int vector, int y);
```
Creates a new Vector3Int with the specified Y component, retaining the original X and Z components.

### WithZ
```csharp
public static Vector3Int WithZ(this Vector3Int vector, int z);
```
Creates a new Vector3Int with the specified Z component, retaining the original X and Y components.

### SetX
```csharp
public static void SetX(this ref Vector3Int vector, int x);
```
Sets the X component of the Vector3Int.

### SetY
```csharp
public static void SetY(this ref Vector3Int vector, int y);
```
Sets the Y component of the Vector3Int.

### SetZ
```csharp
public static void SetZ(this ref Vector3Int vector, int z);
```
Sets the Z component of the Vector3Int.

### WithX
```csharp
public static Vector2 WithX(this Vector2 vector, float x);
```
Creates a new Vector2 with the specified X component, retaining the original Y component.

### WithY
```csharp
public static Vector2 WithY(this Vector2 vector, float y);
```
Creates a new Vector2 with the specified Y component, retaining the original X component.

### SetX
```csharp
public static void SetX(this ref Vector2 vector, float x);
```
Sets the X component of the Vector2.

### SetY
```csharp
public static void SetY(this ref Vector2 vector, float y);
```
Sets the Y component of the Vector2.

### WithX
```csharp
public static Vector2Int WithX(this Vector2Int vector, int x);
```
Creates a new Vector2Int with the specified X component, retaining the original Y component.

### WithY
```csharp
public static Vector2Int WithY(this Vector2Int vector, int y);
```
Creates a new Vector2Int with the specified Y component, retaining the original X component.

### SetX
```csharp
public static void SetX(this ref Vector2Int vector, int x);
```
Sets the X component of the Vector2Int.

### SetY
```csharp
public static void SetY(this ref Vector2Int vector, int y);
```
Sets the Y component of the Vector2Int.

</details>

## HexVectorRandomOps

<details>
<summary>HexVectorRandomOps Functions</summary>

### Random3D
```csharp
public static Vector3 Random3D();
```
Generates a random Vector3 with values between -1 and 1.

### Random2D
```csharp
public static Vector2 Random2D();
```
Generates a random Vector2 with values between -1 and 1.

</details>

## HexVectorUtils

<details>
<summary>HexVectorUtils Functions</summary>

### VectorToColor
```csharp
public static Color VectorToColor(this Vector3 vector, float a = 1.0f);
```
Converts a `Vector3` to a `Color`, using the vector's components for RGB and an optional alpha value.

### ColorToVector
```csharp
public static Vector3 ColorToVector(this Color color);
```
Converts a `Color` to a `Vector3`, using the color's RGB components.

### ConvertTo2D
```csharp
public static Vector2 ConvertTo2D(this Vector3 vector3);
```
Converts a `Vector3` to a `Vector2`, ignoring the Z component.

### ConvertTo3D
```csharp
public static Vector2 ConvertTo3D(this Vector2 vector2);
```
Converts a `Vector2` to a `Vector3`, setting Z to 0.

### ConvertTo2D
```csharp
public static Vector2 ConvertTo2D(this Vector2Int vector2Int);
```
Converts a `Vector2Int` to a `Vector2`.

### ConvertTo3D
```csharp
public static Vector2 ConvertTo3D(this Vector3Int vector3Int);
```
Converts a `Vector3Int` to a `Vector3`, setting Z to 0.

### SqrDistance
```csharp
public static float SqrDistance(this Vector2 a, Vector2 b);
```
Calculates the squared distance between two `Vector2` instances.

### SqrDistance
```csharp
public static float SqrDistance(this Vector2Int a, Vector2Int b);
```
Calculates the squared distance between two `Vector2Int` instances.

### SqrDistanceXY
```csharp
public static float SqrDistanceXY(this Vector3 a, Vector3 b);
```
Calculates the squared distance between two `Vector3` instances in 2D space (XY-plane).

### SqrDistance
```csharp
public static float SqrDistance(this Vector3 a, Vector3 b);
```
Calculates the squared distance between two `Vector3` instances.

### NearlyEquals
```csharp
public static bool NearlyEquals(this Vector3 a, Vector3 b, double inaccuracy = 1.0E-7);
```
Checks if two `Vector3` instances are nearly equal based on an inaccuracy tolerance.

### NearlyEquals
```csharp
public static bool NearlyEquals(this Vector2 a, Vector2 b, double inaccuracy = 1.0E-7);
```
Checks if two

 `Vector2` instances are nearly equal based on an inaccuracy tolerance.

</details>

