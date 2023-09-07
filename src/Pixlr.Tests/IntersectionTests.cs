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
}