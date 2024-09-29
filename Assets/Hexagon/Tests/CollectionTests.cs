using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class CollectionTests
{
    [Test]
    public void AreListsEqual()
    {
        List<int> a = new List<int>() { 1, 9, -1, 22, 9, 9, 0, 4 };
        List<int> b = new List<int>() { 1, 9, -1, 22, 9, 9, 0, 4 }; // identical to A
        List<int> c = new List<int>() { -1, 9, 1, 9, 9, 22, 4, 0 }; // same as A but elements are in another order
        List<int> d = new List<int>() { -1, 9, 1, 22, 4, 0 }; // same set of elements as in A but fewer repetitions
        List<int> e = new List<int>() { -1, 9, 1, 22, 0, 0, 0, 0 }; // other elements but same length

        Assert.That(a.AreListsEqual(a), "A equals to A");
        Assert.That(a.AreListsEqual(b), "A equals to B");
        Assert.That(!a.AreListsEqual(c), "A equals to C");
        Assert.That(!a.AreListsEqual(d), "A equals to D");
        Assert.That(!c.AreListsEqual(d), "C equals to D");
        Assert.That(!a.AreListsEqual(e), "A equals to E");
    }

    [Test]
    public void AreSetsEqual()
    {
        List<int> a = new List<int>() { 1, 9, -1, 22, 9, 9, 0, 4 };
        List<int> b = new List<int>() { 1, 9, -1, 22, 9, 9, 0, 4 }; // identical to A
        List<int> c = new List<int>() { -1, 9, 1, 9, 9, 22, 4, 0 }; // same as A but elements are in another order
        List<int> d = new List<int>() { -1, 9, 1, 22, 4, 0 }; // same set of elements as in A but fewer repetitions
        List<int> e = new List<int>() { -1, 9, 1, 22, 0, 0, 0, 0 }; // other elements but same length

        Assert.That(a.AreSetsEqual(a), "A set equals to A set");
        Assert.That(a.AreSetsEqual(b), "A set equals to B set");
        Assert.That(a.AreSetsEqual(c), "A set equals to C set");
        Assert.That(a.AreSetsEqual(d), "A set equals to D set");
        Assert.That(!a.AreSetsEqual(e), "A set equals to E set");
        Assert.That(!d.AreSetsEqual(e), "D set equals to E set");
    }

    [Test]
    public void AreMultisetsEqual()
    {
        List<int> a = new List<int>() { 1, 9, -1, 22, 9, 9, 0, 4 };
        List<int> b = new List<int>() { 1, 9, -1, 22, 9, 9, 0, 4 }; // identical to A
        List<int> c = new List<int>() { -1, 9, 1, 9, 9, 22, 4, 0 }; // same as A but elements are in another order
        List<int> d = new List<int>() { -1, 9, 1, 22, 4, 0 }; // same set of elements as in A but fewer repetitions
        List<int> f = new List<int>() { -1, 22, 0, 4, 9, 1 }; // same set of elements as in E in another order

        Assert.That(a.AreMultisetsEqual(a), "A multiset equals to A multiset");
        Assert.That(a.AreMultisetsEqual(b), "A multiset equals to B multiset");
        Assert.That(a.AreMultisetsEqual(c), "A multiset equals to C multiset");
        Assert.That(!a.AreMultisetsEqual(d), "A multiset equals to C multiset");
        Assert.That(d.AreMultisetsEqual(f), "D multiset equals to F multiset");
        Assert.That(!c.AreMultisetsEqual(d), "C multiset equals to D multiset");
    }
}