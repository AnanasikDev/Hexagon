using NUnit.Framework;
using System.Runtime.CompilerServices;
using UnityEngine;

[TestFixture]
public class VectorTests
{
    [Test]
    public void Vector_Rotate45()
    {
        var vec = new Vector2(1, 0);
        var vecRotated = vec.Rotate(45);

        Assert.That(vecRotated.magnitude == vec.magnitude);
        Assert.That(vecRotated.x > 0 && vecRotated.y > 0);
    }

    [Test]
    public void Vector_Rotate180()
    {
        var vec = new Vector2(1, 0);
        var vecRotated = vec.Rotate(180);

        Assert.That(vecRotated.magnitude == vec.magnitude);
        Assert.That(vecRotated == -vec);
    }

    [Test]
    public void Vector_Rotate360()
    {
        var vec = new Vector2(-1, 0);
        var vecRotated = vec.Rotate(360);

        Assert.That(vecRotated.magnitude == vec.magnitude);
        Assert.That(vecRotated == vec);
    }

    [Test]
    public void Vector_RotateMagnitude()
    {
        var vec = new Vector2(123, 0);
        var vecRotated = vec.Rotate(48);

        Assert.That(vecRotated.magnitude.NearlyEquals(vec.magnitude));
    }

    [Test]
    public void Vector_Rotate3D()
    {
        Vector3 v1 = new Vector3(0, 0, 1);
        Assert.That(v1.Rotate(Vector3.up, 45).NearlyEquals(new Vector3(Hexath.sqrt2half, 0, Hexath.sqrt2half)));
        Assert.That(v1.Rotate(Vector3.up, 90).NearlyEquals(new Vector3(1, 0, 0)));
        Assert.That(v1.Rotate(Vector3.up, 0).NearlyEquals(v1));
        Assert.That(v1.Rotate(Vector3.up, 360).NearlyEquals(v1));
        Assert.That(v1.Rotate(Vector3.up, 180).NearlyEquals(-v1));

        Assert.That(v1.Rotate(new Vector3(1, 1, 1), 120).NearlyEquals(new Vector3(1, 0, 0)));
        Assert.That(v1.Rotate(new Vector3(1, 1, 1), 240).NearlyEquals(new Vector3(0, 1, 0)));
    }
}
