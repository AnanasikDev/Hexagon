using Hexagon;
using NUnit.Framework;
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

        Assert.That(vecRotated.magnitude.NearlyEqualsPositive(vec.magnitude));
    }

    [Test]
    public void Vector_Rotate3D()
    {
        Vector3 v1 = new Vector3(0, 0, 1);
        Assert.That(v1.Rotate(Vector3.up, 45).NearlyEquals(new Vector3(Hexath.HALF_SQRT_2, 0, Hexath.HALF_SQRT_2)));
        Assert.That(v1.Rotate(Vector3.up, 90).NearlyEquals(new Vector3(1, 0, 0)));
        Assert.That(v1.Rotate(Vector3.up, 0).NearlyEquals(v1));
        Assert.That(v1.Rotate(Vector3.up, 360).NearlyEquals(v1));
        Assert.That(v1.Rotate(Vector3.up, 180).NearlyEquals(-v1));

        Assert.That(v1.Rotate(new Vector3(1, 1, 1), 120).NearlyEquals(new Vector3(1, 0, 0)));
        Assert.That(v1.Rotate(new Vector3(1, 1, 1), 240).NearlyEquals(new Vector3(0, 1, 0)));
    }

    [Test]
    public void Vector_SetComponents()
    {
        Vector2 a = new Vector2(1, 0);
        a.SetX(3);
        Assert.AreEqual(a.x, 3);
        Assert.AreEqual(a.WithY(4), new Vector2(3, 4));
        Assert.AreEqual(a.With(x: 1, y: 1), Vector2.one);

        Vector3 b = new Vector3();
        Assert.AreEqual(b.xyz(), b.yzx());
        b.Set(x: 20);
        Assert.AreEqual(b.With(2, 3), new Vector3(2, 3, 0));
        Assert.AreEqual(b, new Vector3(20, 0, 0));
    }
}
