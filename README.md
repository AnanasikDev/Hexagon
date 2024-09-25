# Hexagon

Unity extensions library

## Table of contents

- [Instructions and recommendations](##Instructions-and-recommendations)
- [Features](##Features)
- [HexCoreMath](###HexCoreMath)

## Instructions and recommendations

```HexCoroutineRunner``` should be attached to **any** gameobject on each scene where its functionality is needed

## Features

### HexCoreMath:

```float SnapNumberToStep(float number, float step)```: Snaps the given number to the nearest float number within the given step. Rounding for float-point numbers with adjustable accuracy given as the ```step``` argument.

```Vector2 GetCirclePointDegrees(float radius, float angleDeg)```: Returns a point on the circumference with the given "radius" at the given "angle" in degrees, starting at the point (radius, 0) as in math.