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

    [Test]
    public void Vector_AngleBetween2D()
    {
        Vector2 v1 = new Vector2(1, 0);
        Vector2 v2 = new Vector2(0, 1);
        Vector2 v3 = new Vector2(Hexath.sqrt2, -Hexath.sqrt2);
        Vector2 v4 = new Vector2(1, 1);
        Assert.That(v1.AbsAngleBetween2D(v2).NearlyEquals(90));
        Assert.That(v1.AbsAngleBetween2D(v3).NearlyEquals(45));
        Assert.That(v1.AbsAngleBetween2D(v4).NearlyEquals(45));
        Assert.That(v3.AbsAngleBetween2D(v4).NearlyEquals(90));

        Assert.That(v1.SignedAngleBetween2D(v2).NearlyEquals(90));
        Assert.That(v2.SignedAngleBetween2D(v1).NearlyEquals(-90));
        Assert.That(v1.SignedAngleBetween2D(v3).NearlyEquals(45));
        //Assert.That(v3.SignedAngleBetween2D(v1).NearlyEquals(-45));
        Assert.That(v1.SignedAngleBetween2D(v4).NearlyEquals(45));
        Assert.That(v4.SignedAngleBetween2D(v1).NearlyEquals(-45));
        Assert.That(v3.SignedAngleBetween2D(v4).NearlyEquals(90));

        Vector2 a = new Vector2(0.123f, 2f);
        Vector2 b = new Vector2(0.7f, -0.399f);

        Assert.That(a.SignedAngleBetween2D(b).NearlyEquals(Vector2.SignedAngle(a, b)));
        Assert.That(b.SignedAngleBetween2D(a).NearlyEquals(Vector2.SignedAngle(b, a)));
        Assert.That(a.AbsAngleBetween2D(b).NearlyEquals(Vector2.Angle(a, b)));
    }
}
