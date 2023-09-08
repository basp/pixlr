using System.Numerics;
using System.Xml.Xsl;

namespace Pixlr.Tests;

public class IntersectionTests
{
    [Fact]
    public void AggregatingIntersections()
    {
        var s = new Sphere();
        var i1 = new Intersection(1, s);
        var i2 = new Intersection(2, s);
        var xs = new[] { i1, i2 }
            .Order()
            .ToList();
        Assert.Equal(1, xs[0].T);
        Assert.Equal(2, xs[1].T);
    }

    [Fact]
    public void TheHitWhenAllIntersectionsHavePositiveT()
    {
        var s = new Sphere();
        var i1 = new Intersection(1, s);
        var i2 = new Intersection(2, s);
        var xs = new[] { i1, i2 };
        Assert.True(xs.TryGetHit(out var i));
        Assert.Equal(i1, i);
    }

    [Fact]
    public void TheHitWhenSomeIntersectionsHaveNegativeT()
    {
        var s = new Sphere();
        var i1 = new Intersection(-1, s);
        var i2 = new Intersection(1, s);
        var xs = new[] { i2, i1 };
        Assert.True(xs.TryGetHit(out var i));
        Assert.Equal(i2, i);
    }

    [Fact]
    public void TheHitWhenAllIntersectionsHaveNegativeT()
    {
        var s = new Sphere();
        var i1 = new Intersection(-2, s);
        var i2 = new Intersection(-1, s);
        var xs = new[] { i2, i1 };
        Assert.False(xs.TryGetHit(out var i));
    }

    [Fact]
    public void TheHitIsTheLowestNonnegativeIntersection()
    {
        var s = new Sphere();
        var xs = new[]
        {
            new Intersection(5, s),
            new Intersection(7, s),
            new Intersection(-3, s),
            new Intersection(2, s),
        };
        Assert.True(xs.TryGetHit(out var i));
        Assert.Equal(xs[3], i);
    }

    [Fact]
    public void ComputingInteractionFromIntersection()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 0, -5),
            Vector4.CreateDirection(0, 0, 1));
        var shape = new Sphere();
        var i = new Intersection(4, shape);
        var comps = Interaction.FromIntersection(i, r);
        Assert.Equal(i.Obj, comps.Object);
        Assert.Equal(
            Vector4.CreatePosition(0, 0, -1), 
            comps.Point);
        Assert.Equal(
            Vector4.CreateDirection(0, 0, -1),
            comps.Eye);
        Assert.Equal(
            Vector4.CreateDirection(0, 0, -1),
            comps.Normal);
    }

    [Fact]
    public void TheHitWhenAnIntersectionOccursOnTheOutside()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 0, -5),
            Vector4.CreateDirection(0, 0, 1));
        var shape = new Sphere();
        var i = new Intersection(4, shape);
        var intr = Interaction.FromIntersection(i, r);
        Assert.False(intr.Inside);
    }

    [Fact]
    public void TheHitWhenAnIntersectionOccursOnTheInside()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 0, 0),
            Vector4.CreateDirection(0, 0, 1));
        var shape = new Sphere();
        var i = new Intersection(1, shape);
        var intr = Interaction.FromIntersection(i, r);
        Assert.Equal(
            Vector4.CreatePosition(0, 0, 1), 
            intr.Point);
        Assert.Equal(
            Vector4.CreateDirection(0, 0, -1),
            intr.Eye);
        Assert.Equal(
            Vector4.CreateDirection(0, 0, -1),
            intr.Normal);
        Assert.True(intr.Inside);
    }
}