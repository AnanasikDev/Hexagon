using NUnit.Framework;

[TestFixture]
public class HexathTests
{
    [Test]
    public void LCM_Two_Test()
    {
        Assert.AreEqual(12, Hexath.LeastCommonMultiple(3, 4));
        Assert.AreEqual(4, Hexath.LeastCommonMultiple(2, 4));
        Assert.AreEqual(2, Hexath.LeastCommonMultiple(2, 2));
        Assert.AreEqual(81, Hexath.LeastCommonMultiple(9, 81));
        Assert.AreEqual(81, Hexath.LeastCommonMultiple(81, 3));
        Assert.AreEqual(55, Hexath.LeastCommonMultiple(11, 5));
    }

    [Test]
    public void GCD_Two_Test()
    {
        Assert.AreEqual(1, Hexath.GreatestCommonFactor(2, 3));
        Assert.AreEqual(2, Hexath.GreatestCommonFactor(10, 4));
        Assert.AreEqual(2, Hexath.GreatestCommonFactor(10, 24));
        Assert.AreEqual(8, Hexath.GreatestCommonFactor(16, 24));
        Assert.AreEqual(1, Hexath.GreatestCommonFactor(16, 25));
        Assert.AreEqual(1, Hexath.GreatestCommonFactor(25, 16));
    }

    [Test]
    public void LCM_Multiple_Test()
    {
        Assert.AreEqual(6, Hexath.LeastCommonMultiple(new int[] { 1, 2, 3 }));
        Assert.AreEqual(10, Hexath.LeastCommonMultiple(new int[] { 5, 1, 2 }));
        Assert.AreEqual(3, Hexath.LeastCommonMultiple(new int[] { 1, 3, 1 }));
        Assert.AreEqual(24, Hexath.LeastCommonMultiple(new int[] { 8, 4, 12 }));
        Assert.AreEqual(24, Hexath.LeastCommonMultiple(new int[] { 1, 2, 4, 6, 8, 12, 3, 1, 3, 12 }));
        Assert.AreEqual(1, Hexath.LeastCommonMultiple(new int[] { 1 }));
        Assert.AreEqual(1, Hexath.LeastCommonMultiple(new int[] { 1, 1 }));
        Assert.AreEqual(9, Hexath.LeastCommonMultiple(new int[] { 1, 9 }));
    }

    [Test]
    public void GCD_Multiple_Test()
    {
        Assert.AreEqual(1, Hexath.GreatestCommonFactor(new int[] { 7, 11, 41 }));
        Assert.AreEqual(2, Hexath.GreatestCommonFactor(new int[] { 8, 10, 12 }));
        Assert.AreEqual(4, Hexath.GreatestCommonFactor(new int[] { 12, 8 }));
        Assert.AreEqual(4, Hexath.GreatestCommonFactor(new int[] { 8, 12, 100, 4, 8, 28 }));
        Assert.AreEqual(1, Hexath.GreatestCommonFactor(new int[] { 7, 50, 700 }));
    }
}