# Hexagon

Unity extensions library

## Table of contents

- [Instructions and recommendations](##Instructions-and-recommendations)
- [Features](#Features)
    * [Math](#HexMath)
    * [Time](#HexTime)

## Instructions and recommendations

```HexCoroutineRunner``` should be attached to **any** gameobject on each scene where its functionality is needed

## Features

### HexMath:

```csharp
float SnapNumberToStep(float number, float step)
```
: Snaps the given number to the nearest float number within the given step. Rounding for float-point numbers with adjustable accuracy given as the ```step``` argument.

```csharp
Vector2 GetCirclePointDegrees(float radius, float angleDeg)
```

Returns a point on the circumference with the given "radius" at the given "angle" in degrees, starting at the point (radius, 0) as in math.

### HexTime:

```csharp
void InvokeDelayed(float delaySeconds, System.Action action)
```
with variations for void output and 0, 1, 2, 3, 4 generic arguments and 1 output with 0, 1, 2, 3 generic arguments

```csharp
IEnumerator WaitForCondition(System.Func<bool> condition, System.Action onSuccess)
``` 
Invokes the input function once the condition() becomes true

### Vector:
#### Vector3

```csharp
Vector3 Multiply(this Vector3 a, Vector3 b)
```

```csharp
Vector3 Divide(this Vector3 a, Vector3 b)
```

```csharp
Vector3 Abs(this Vector3 vector)
```

```csharp

```