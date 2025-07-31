# Hexagon

Unity extensions library

## Table of contents

- [Notes](#Notes)
- [Features](#Features)
    * [Math](#Math)
    * [Time](#Time)
    * [Vector](#Vector)
    * [Random](#Random)
    * [Transform](#Transform)
    * [Collections](#Collections)
    * [Pool](#Pool)
    * [Debug](#Debug)
    * [Easing](#Easing)
- [Tests](#Tests)

## Notes

```HexCoroutineRunner``` should be attached to **any** gameobject on each scene where its functionality is needed (used to use Unity built-in coroutines from non-MonoBehaviour classes). As it is using singleton there should be no more than one instance of it on a scene (there is no reason to have more anyway).

All other classes (except for ```Pool``` and ```HexCoroutineRunner```) and functions are ```static``` and are declared in the global namespace. 

## Features

### Math

```csharp 
float SnapNumberToStep(float number, float step)
```
Snaps the given number to the nearest float number within the given step. Rounding for float-point numbers with adjustable accuracy given as the ```step``` argument.

```csharp
Vector2 GetCirclePointDegrees(float radius, float angle)
```
Returns a point on the circumference with the given "radius" at the given "angle" in degrees, starting at the point (radius, 0) as in math.

```csharp
Vector2 GetCirclePointRadians(float radius, float angle)
```
Returns a point on the circumference with the given "radius" at the given "angle" in radians, starting at the point (radius, 0) as in math.

```csharp
Vector2 GetRandomRingPoint(float radius)
```
Returns random point on the circumference of the given "radius".

```csharp
Vector2 GetRandomCirclePoint(float radius)
```
Returns random point on or within the circumference of the given "radius".

```csharp
int Ternarsign(float value)
```
Returns -1 if value is negative, 0 if value is 0, 1 if value is positive

```csharp
float Ramp(float value, float min, float max)
```
Holds the input "value" at "max" when it is larger than "min", otherwise starts decreasing starting from "max".

```csharp
float MinLimit(float value, float min)
```
Returns value if it's greater than min threshold, min otherside

```csharp
float MaxLimit(float value, float max)
```
Returns value if it's less than max threshold, or max otherwise

### Time

Note: All coroutine-related functions work using ```HexCoroutineRunner``` script that has to be attached to any gameobject on scene, unless non of these functions are called.

```csharp
void InvokeDelayed(float delaySeconds, System.Action action)
```
Invokes the given function with a delay. There are overloads for void output and 0, 1, 2, 3, 4 generic arguments and 1 output with 0, 1, 2, 3 generic arguments.

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
Vector3 Multiply(this Vector3 a, Vector3 b)
Vector3Int Multiply(this Vector3Int a, Vector3Int b)
Vector2 Multiply(this Vector2 a, Vector2 b)
Vector2Int Multiply(this Vector2Int a, Vector2Int b)
```
Multiplies each component of the first vector by the corresponding component of the second vector, returning a new vector with the result.

### Divide (Element-wise)
```csharp
Vector3 Divide(this Vector3 a, Vector3 b)
Vector3Int Divide(this Vector3Int a, Vector3Int b)
Vector2 Divide(this Vector2 a, Vector2 b)
Vector2Int Divide(this Vector2Int a, Vector2Int b)
```
Divides each component of the first vector by the corresponding component of the second vector, returning a new vector with the result.  
> **Remarks:** Not safe from division by zero

### Abs (Per component)
```csharp
Vector3 Abs(this Vector3 vector)
Vector3Int Abs(this Vector3Int vector)
Vector2 Abs(this Vector2 vector)
Vector2Int Abs(this Vector2Int vector)
```
Returns a new vector where each component is the absolute value of the corresponding component in the original vector.

</details>

<details>
<summary>HexVectorAxisOps</summary>

### Overview
These functions allow inline manipulation of individual components of vectors. They provide a way to set values for individual axes (X, Y, Z).

### Set Axis Value
```csharp
void SetX(this ref Vector3 vector, float x)
void SetY(this ref Vector3 vector, float y)
void SetZ(this ref Vector3 vector, float z)
void SetX(this ref Vector3Int vector, int x)
void SetY(this ref Vector3Int vector, int y)
void SetZ(this ref Vector3Int vector, int z)
void SetX(this ref Vector2 vector, float x)
void SetY(this ref Vector2 vector, float y)
void SetX(this ref Vector2Int vector, int x)
void SetY(this ref Vector2Int vector, int y)
```
Directly modify a specific component (X, Y, or Z) of the given vector.

### With Axis Value
```csharp
Vector3 WithX(this Vector3 vector, float x)
Vector3 WithY(this Vector3 vector, float y)
Vector3 WithZ(this Vector3 vector, float z)
Vector3Int WithX(this Vector3Int vector, int x)
Vector3Int WithY(this Vector3Int vector, int y)
Vector3Int WithZ(this Vector3Int vector, int z)
Vector2 WithX(this Vector2 vector, float x)
Vector2 WithY(this Vector2 vector, float y)
Vector2Int WithX(this Vector2Int vector, int x)
Vector2Int WithY(this Vector2Int vector, int y)
```
Create a new vector where the specified component is replaced with the given value, keeping the original vector object unchanged.

### Null Z
```csharp
Vector3 NullZ(this Vector3 vector)
```
A shortcut for ```WithZ(0)```

</details>

<details>
<summary>HexVectorRandomOps</summary>

### Random Vectors
```csharp
Vector3 Random3D()
Vector2 Random2D()
```
Generates a random vector where each component is a random float value between -1 and 1.

</details>

<details>
<summary>HexVectorUtils</summary>

### Conversion
```csharp
Color VectorToColor(this Vector3 vector, float a = 1.0f)
Vector3 ColorToVector(this Color color)
Vector2 ConvertTo2D(this Vector3 vector3)
Vector2 ConvertTo2D(this Vector2Int vector2Int)
Vector3 ConvertTo3D(this Vector2 vector2)
Vector3 ConvertTo3D(this Vector3Int vector3Int)
```
- `VectorToColor`: Converts a `Vector3` to a `Color`, mapping the vector's components to RGB channels, and optionally setting the alpha channel.
- `ColorToVector`: Converts a `Color` back to a `Vector3`, using the RGB values.

```csharp
Vector2Int RoundToInt(this Vector2 vector2)
Vector3Int RoundToInt(this Vector3 vector3)
Vector2Int CeilToInt(this Vector2 vector2)
Vector3Int CeilToInt(this Vector3 vector3)
Vector2Int FloorToInt(this Vector2 vector2)
Vector3Int FloorToInt(this Vector3 vector3)
```
Convert VectorN to VectorNInt

### Distance Calculation
```csharp
float SqrDistance(this Vector2 a, Vector2 b)
float SqrDistance(this Vector2Int a, Vector2Int b)
float SqrDistance(this Vector3 a, Vector3 b)
float SqrDistance(this Vector3Int a, Vector3Int b)
float SqrDistanceXY(this Vector3 a, Vector3 b)
```
Calculate the squared distance between two vectors. `SqrDistanceXY` calculates the squared distance on XY plane.

```csharp
float Distance(this Vector2 a, Vector2 b)
float Distance(this Vector2Int a, Vector2Int b)
float Distance(this Vector3 a, Vector3 b)
float Distance(this Vector3Int a, Vector3Int b)
float DistanceXY(this Vector3 a, Vector3 b)
```
Calculate the real distance between two vectors. `DistanceXY` calculates the real distance on XY plane.

### Comparison
```csharp
bool NearlyEquals(this Vector3 a, Vector3 b, double inaccuracy = 1.0E-7)
bool NearlyEquals(this Vector2 a, Vector2 b, double inaccuracy = 1.0E-7)
```
Checks if two vectors are nearly equal, allowing for a small tolerance (inaccuracy) to account for floating-point precision errors.

### Rotation
```csharp
Vector2 Rotate(this Vector2 vector, float degrees)
```
Rotates the given ```Vector2``` by the given degree and returns the result without changing original vector.

</details>

<details>
<summary>HexVectorMath</summary>

```csharp
Vector2 Clamp01(this Vector2 vector)
```
Clamps the given ```Vector2``` to 0.0 - 1.0 range

```csharp
Vector3 Clamp01(this Vector3 vector)
```
Clamps the given ```Vector3``` to 0.0 - 1.0 range

</details>

## Random

`HexRandom` is a static utility class providing a comprehensive suite of randomization helpers for Unity-based projects. It extends Unity’s `Random` functionality with concise, readable, and assert-checked methods for collections, booleans, signs, vectors, weighted distributions, strings, colors, and geometric points.

## Features

- Random selection and shuffling of collections
- Controlled boolean and sign generation
- Random vector and color generation
- Weighted index selection
- Enum value randomization
- Random character and substring extraction
- Geometric point generation within Unity shapes

## Collections

- `T RandomElement<T>(this IList<T> collection)`
  - Returns a random element from a non-empty collection.
  - Asserts: collection is not null and not empty.

- `T RandomElementWithIndex<T>(this IList<T> collection, out int index)`
  - Returns a random element and its index from a non-empty collection.

- `T Select<T>(params T[] values)`
  - Shorthand for choosing a random value from the provided arguments.

- `IEnumerable<T> GetRandomElements<T>(this IList<T> collection, int num, bool unique = true)`
  - Returns `num` elements, optionally unique.
  - Shuffles when `unique = true`; otherwise, samples.

- `void Shuffle<T>(this IList<T> list)`
  - In-place Fisher-Yates shuffle.

- `List<T> GetShuffled<T>(this IList<T> list)`
  - Returns a new shuffled list copy.

## Booleans

- `bool GetBool(float trueBias, float falseBias)`
  - Returns a boolean with a probability bias.
  - `trueBias` : `falseBias` determines output ratio
  - No checks for zero denominator (`trueBias + falseBias`).

- `bool GetBool(float minTrue = 0.5f)`
  - Returns `true` with chance `1 - minTrue`.

## Signs

- `int GetSign(float minPositive = 0.5f)`
  - Returns `+1` or `-1`, with `+1` probability being `1 - minPositive`.

- `int GetTernarSign()`
  - Uniformly returns `-1`, `0`, or `+1`.

- `int GetTernarSign(float negBias, float zeroBias, float posBias)`
  - Returns ternary value based on segment weights.

## Vectors

- `Vector3 Random3D()`
- `Vector2 Random2D()`
  - Each component is in range `[-1, 1]`.

## Weighted Selection

- `int GetWeightedIndex(float[] segments, float sum)`
- `int GetWeightedIndex(float[] segments)`
  - Selects index based on relative segment weight.
  - No validation for non-positive weights.

## Colors

- `Color RandomColorOpaque()`
  - RGB in `[0,1]`, alpha = 1.

- `Color RandomColor(bool randomAlpha = false)`
  - RGB in `[0,1]`; alpha optional random.

- `Color RandomColor(float min, float max)`
  - Each channel in `[min, max]`.

## Random Points

- `Vector3 RandomPointOnSphere(float radius)`
- `Vector3 RandomPointInSphere(float radius)`
- `Vector2 RandomPointOnCircle(float radius, float maxAngle = 2π)`
- `Vector2 RandomPointInCircle(float radius, float maxAngle = 2π)`
  - Angle optional for partial arc selection.

- `Vector3 RandomPointInBounds(this Bounds bounds)`
- `Vector2 RandomPointInRect(this Rect rect)`
- `Quaternion RandomRotation2D()`

## Enums

- `T RandomEnumValue<T>() where T : Enum`
  - Returns a random enum value.
  - Asserts that the enum is not empty.

## Strings

- `char GetRandomChar(this string str)`
  - Picks a random character from a non-empty string.

- `string GetRandomSubstring(this string str)`
  - Returns a substring of random length and position.

- `string GetRandomSubstringOfLength(this string str, int length)`
  - Returns a substring of specified length at random position.
  - Validates length bounds.


## Transform

```csharp
List<Transform> GetChildren(this Transform transform)
```
Iterates over all transforms attached to this transform as direct children. For recursive search see ```GetChildrenRecursive```

```csharp
List<Transform> GetChildrenRecursive(this Transform transform)
```
Recursively iterates over all transforms nested to this transform

## Audio

Default random settings:
```csharp
float pitch_min = 0.94f;
float pitch_max = 1.04f;
float volume_min = 0.97f;
float volume_max = 1.03f;
```

```csharp
float RandomizePitch(this AudioSource source)
```
Sets and returns random pitch for the audio source.

```csharp
float RandomizeVolume(this AudioSource source)
```
Sets and returns random volume for the audio source.

## Collections

```csharp
T RandomElement<T>(this T[] array)
```
Returns random element from the given array with the scope of [first, last].

```csharp
T RandomElement<T>(this List<T> list)
```
Returns random element from the given list with the scope of [first, last].

```csharp
bool AreListsEqual<T>(this List<T> list1, List<T> list2)
```
Checks if two lists are completely equal (same length, same objects, and same order).

```csharp
bool AreSetsEqual<T>(this List<T> list1, List<T> list2)
```
Checks if two lists contain the same unique objects, regardless of order or the number of occurrences.

```csharp
bool AreMultisetsEqual<T>(this List<T> list1, List<T> list2)
```
Checks if two lists contain the same objects with the same number of occurrences, regardless of order.

## Pool

### Pool\<T> where T : class

A generic, one-list object manager. This class maintains a single collection of objects and uses a delegate to determine whether each object is currently "active" or "inactive".

It is designed for scenarios where objects have a distinct active/inactive state that **is managed externally**, and the primary goal is to reuse inactive objects rather than creating new ones.

### Core Concepts

The pool's behavior is defined by four primary delegates provided during construction:

1. **factory**: Creates a new object instance when no inactive objects are available. It is also responsible for managing the initial state.

2. **isActive**: Determines if the given object is active or inactive. This is the central mechanism for state tracking.

3. **onGet**: Transitions the gotten object from inactive to active. Make sure that `isActive` will treat the newly gotten object as active.

4. **onRelease**: Transitions the gotten object from active to inactive. Make sure that `isActive` will treat the released object as inactive.

---

Note: `isActive` **MUST** be synchronized with `onGet` and `onRelease`. The state is **NOT** tracked internally.

### Initialization

Create a pool instance using the static `Create` method, providing the necessary delegates.

```cs
public static Pool<T> Create(
    Func<T> factory,
    Action<T> onGet, 
    Action<T> onRelease,
    Func<T, bool> isActive,
    int listCapacity = 20
)
```

Example:
```cs
Pool<GameObject> pool = Pool<GameObject>.Create(
    factory: () =>
    {
        GameObject result = new GameObject("My gameobject");
        result.SetActive(false);
        return result;
    },
    onGet:      (GameObject item) => item.SetActive(true),
    onRelease:  (GameObject item) => item.SetActive(false),
    isActive:   (GameObject item) => item.activeSelf
);
```

### Recommended API
The primary and recommended way to retrieve an object is with the `Get()` method.

`Get()` Searches for the first available inactive object. If one is found, it is returned. If not, a new object is created using the `factory`, added to the pool, and then returned. `onGet` action is called automatically and the returned object is ready to use.

`Release(T item)` Releases the specified object and calls OnRelease action on it.

`ReleaseAll()` Releases all inactive objects.

`Populate(int delta)` Creates `delta` **new** objects and adds them to the pool. Their state is set by `factory` delegate.

### Extra API

Not recommended for general use. Must be used with caution.

`ViewExistingInactive(out T result)` Finds the first inactive object without changing its state. To finalize getting, `Get(item)` must be called. To discard getting no actions required.

`ViewExistingActive(out T result)` Finds the first active object without changing its state. To finalize releasing, `Release(item)` must be called. To discard releasing no actions required.

`Get(T item)` marks the specified object as gotten.

### Important Notes

#### External State Management

This pool does not internally manage the state of objects. It relies entirely on the `isActive` delegate to report the state. The user is responsible for ensuring that the `onGet` and `onRelease` actions correctly modify the object's state so that `isActive` returns the expected value.

#### Linear Search Performance

All `Get` and `View` operations perform a linear search through the collection. Performance will degrade as the total number of objects in the pool increases. This implementation is best suited for small collections.

## Debug

```csharp
public static class HexDebug
```

```csharp
void Log(params object[] objs)
```
Logs multiple objects in the Console (leaving space between them)

```csharp
void LogWarning(params object[] objs)
```

Logs as a warning multiple objects in the Console (leaving space between them)

```csharp
void LogError(params object[] objs)
```

Logs as an error multiple objects in the Console (leaving space between them)

# Easing

```cs
public static class HexEasing
```

from [github](https://gist.github.com/ManeFunction/9f2d437fca6ccf31e4a48fec0584e21a) *Copyright (c)2001 Robert Penner*

## Functions:
- EaseInQuad,
- EaseOutQuad,
- EaseInOutQuad,
- EaseInCubic,
- EaseOutCubic,
- EaseInOutCubic,
- EaseInQuart,
- EaseOutQuart,
- EaseInOutQuart,
- EaseInQuint,
- EaseOutQuint,
- EaseInOutQuint,
- EaseInSine,
- EaseOutSine,
- EaseInOutSine,
- EaseInExpo,
- EaseOutExpo,
- EaseInOutExpo,
- EaseInCirc,
- EaseOutCirc,
- EaseInOutCirc,
- Linear,
- Spring,
- EaseInBounce,
- EaseOutBounce,
- EaseInOutBounce,
- EaseInBack,
- EaseOutBack,
- EaseInOutBack,
- EaseInElastic,
- EaseOutElastic,
- EaseInOutElastic,

# Strings

```cs
public static class HexString
```

Use these methods for fast variable substitution without format specifiers. All `{var}` placeholders are replaced with the corresponding string value.

```csharp
string template = "Hello, {name}!";
string result = template.InplaceVariables("name", "Alice");
// output: Hello, Alice!
```

```csharp
string template = "Hello, {value:F2}!";
string result = template.FormatVariables("value", 123.917f);
// output: Hello, 123.92!
```

which is equivalent to:

```csharp
string template = "Hello, {value:F2}!";
string result = template.FormatVariables
(
    new Dictionary<string, object>() 
    {
        { "value", 123.917f }
    }
);
// output: Hello, 123.92!
```

# Tests

Tests are implemented using built-in Unity NUnit Framework.