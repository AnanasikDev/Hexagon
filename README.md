# Hexagon

Unity extensions library

## Table of contents

- [Notes](#Notes)
- [Features](#Features)
    * [Math](#Math)
    * [Time](#Time)
    * [Vector](#Vector)
    * [Transform](#Transform)

## Notes

```HexCoroutineRunner``` should be attached to **any** gameobject on each scene where its functionality is needed (used to use Unity built-in coroutines from non-MonoBehaviour classes). As it is using singleton there should be no more than one instance of it on a scene (there is no reason to have more anyway).

## Features

### Math

```csharp 
float SnapNumberToStep(float number, float step)
```
Snaps the given number to the nearest float number within the given step. Rounding for float-point numbers with adjustable accuracy given as the ```step``` argument.

```csharp
Vector2 GetCirclePointDegrees(float radius, float angleDeg)
```
Returns a point on the circumference with the given "radius" at the given "angle" in degrees, starting at the point (radius, 0) as in math.

### Time

```csharp
void InvokeDelayed(float delaySeconds, System.Action action)
```
exists for void output and 0, 1, 2, 3, 4 generic arguments and 1 output with 0, 1, 2, 3 generic arguments

```csharp
void InvokeOnCondition(System.Func<bool> condition, System.Action action)
```
Invokes the input function once the condition() becomes true

## Vector

<details>
<summary>HexVectorOps</summary>

### Overview
These functions perform element-wise arithmetic operations on vectors - multiplication, division, and taking absolute values. They work on `Vector3`, `Vector3Int`, `Vector2`, and `Vector2Int` types.

### Multiply (Element-wise)
```csharp
Vector3 Multiply(this Vector3 a, Vector3 b);
Vector3Int Multiply(this Vector3Int a, Vector3Int b);
Vector2 Multiply(this Vector2 a, Vector2 b);
Vector2Int Multiply(this Vector2Int a, Vector2Int b);
```
Multiplies each component of the first vector by the corresponding component of the second vector, returning a new vector with the result.

### Divide (Element-wise)
```csharp
Vector3 Divide(this Vector3 a, Vector3 b);
Vector3Int Divide(this Vector3Int a, Vector3Int b);
Vector2 Divide(this Vector2 a, Vector2 b);
Vector2Int Divide(this Vector2Int a, Vector2Int b);
```
Divides each component of the first vector by the corresponding component of the second vector, returning a new vector with the result.  
> **Remarks:** Watch out for division by zero in any vector component. Always check the second vector before dividing.

### Abs (Per component)
```csharp
Vector3 Abs(this Vector3 vector);
Vector3Int Abs(this Vector3Int vector);
Vector2 Abs(this Vector2 vector);
Vector2Int Abs(this Vector2Int vector);
```
Returns a new vector where each component is the absolute value of the corresponding component in the original vector.

</details>

<details>
<summary>HexVectorAxisOps</summary>

### Overview
These functions allow inline manipulation of individual components of vectors. They provide a way to set values for individual axes (X, Y, Z).

### Set Axis Value
```csharp
void SetX(this ref Vector3 vector, float x);
void SetY(this ref Vector3 vector, float y);
void SetZ(this ref Vector3 vector, float z);
void SetX(this ref Vector3Int vector, int x);
void SetY(this ref Vector3Int vector, int y);
void SetZ(this ref Vector3Int vector, int z);
void SetX(this ref Vector2 vector, float x);
void SetY(this ref Vector2 vector, float y);
void SetX(this ref Vector2Int vector, int x);
void SetY(this ref Vector2Int vector, int y);
```
Directly modify a specific component (X, Y, or Z) of the given vector.

### With Axis Value
```csharp
Vector3 WithX(this Vector3 vector, float x);
Vector3 WithY(this Vector3 vector, float y);
Vector3 WithZ(this Vector3 vector, float z);
Vector3Int WithX(this Vector3Int vector, int x);
Vector3Int WithY(this Vector3Int vector, int y);
Vector3Int WithZ(this Vector3Int vector, int z);
Vector2 WithX(this Vector2 vector, float x);
Vector2 WithY(this Vector2 vector, float y);
Vector2Int WithX(this Vector2Int vector, int x);
Vector2Int WithY(this Vector2Int vector, int y);
```
Create a new vector where the specified component is replaced with the given value, keeping the original vector object unchanged.

### Null Z
```csharp
Vector3 NullZ(this Vector3 vector);
```
A shortcut for ```WithZ(0)```

</details>

<details>
<summary>HexVectorRandomOps</summary>

### Random Vectors
```csharp
Vector3 Random3D();
Vector2 Random2D();
```
Generates a random vector where each component is a random float value between -1 and 1.

</details>

<details>
<summary>HexVectorUtils</summary>

### Conversion
```csharp
Color VectorToColor(this Vector3 vector, float a = 1.0f);
Vector3 ColorToVector(this Color color);
Vector2 ConvertTo2D(this Vector3 vector3);
Vector2 ConvertTo2D(this Vector2Int vector2Int);
Vector3 ConvertTo3D(this Vector2 vector2);
Vector3 ConvertTo3D(this Vector3Int vector3Int);
```
- `VectorToColor`: Converts a `Vector3` to a `Color`, mapping the vector's components to RGB channels, and optionally setting the alpha channel.
- `ColorToVector`: Converts a `Color` back to a `Vector3`, using the RGB values.

### Distance Calculation
```csharp
float SqrDistance(this Vector2 a, Vector2 b);
float SqrDistance(this Vector2Int a, Vector2Int b);
float SqrDistance(this Vector3 a, Vector3 b);
float SqrDistanceXY(this Vector3 a, Vector3 b);
```
Calculates the squared distance between two vectors. `SqrDistanceXY` calculates squared distance on XY plane.

### Comparison
```csharp
bool NearlyEquals(this Vector3 a, Vector3 b, double inaccuracy = 1.0E-7);
bool NearlyEquals(this Vector2 a, Vector2 b, double inaccuracy = 1.0E-7);
```
Checks if two vectors are nearly equal, allowing for a small tolerance (inaccuracy) to account for floating-point precision errors.

### Rotation
```csharp
Vector2 Rotate(this Vector2 vector, float degrees)
```
Rotates the given Vector2 by the given degree and returns the result without changing original vector.

</details>

## Transform

```csharp
List<Transform> GetChildren(this Transform transform)
```
Iterates over all transforms attached to this transform as direct children. For recursive search see ```GetChildrenRecursive```

```csharp
List<Transform> GetChildrenRecursive(this Transform transform)
```
Recursively iterates over all transforms nested to this transform