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

        Assert.That(vecRotated.magnitude.NearlyEquals(vec.magnitude));
    }
}
