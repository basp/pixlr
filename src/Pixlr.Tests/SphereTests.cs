using System.Runtime.InteropServices.ComTypes;
using NuGet.Frameworks;

namespace Pixlr.Tests;

public class SphereTests
{
    [Fact]
    public void RayIntersectsSphereAtTwoPoints()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 0, -5),
            Vector4.CreateDirection(0, 0, 1));
        var s = new Sphere();
        var xs = s
            .IntersectAll(r)
            .Order()
            .ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(4.0, xs[0].T);
        Assert.Equal(6.0, xs[1].T);
    }

    [Fact]
    public void RayIntersectsSphereAtTangent()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 1, -5),
            Vector4.CreateDirection(0, 0, 1));
        var s = new Sphere();
        var xs = s
            .IntersectAll(r)
            .Order()
            .ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(5.0, xs[0].T);
        Assert.Equal(5.0, xs[1].T);
    }

    [Fact]
    public void RayMissesSphere()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 2, -5),
            Vector4.CreateDirection(0, 0, 1));
        var s = new Sphere();
        var xs = s
            .IntersectAll(r)
            .Order()
            .ToList();
        Assert.Empty(xs);
    }

    [Fact]
    public void RayOriginatesInsideSphere()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 0, 0),
            Vector4.CreateDirection(0, 0, 1));
        var s = new Sphere();
        var xs = s
            .IntersectAll(r)
            .Order()
            .ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(-1, xs[0].T);
        Assert.Equal(+1, xs[1].T);
    }

    [Fact]
    public void SphereIsBehindTheRay()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 0, 5),
            Vector4.CreateDirection(0, 0, 1));
        var s = new Sphere();
        var xs = s
            .IntersectAll(r)
            .Order()
            .ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(-6, xs[0].T);
        Assert.Equal(-4, xs[1].T);
    }

    [Fact]
    public void SphereDefaultTransformation()
    {
        var s = new Sphere();
        var comparer = new Matrix4x4EqualityComparer(1e-6);
        Assert.Equal(
            Matrix4x4.Identity, 
            s.Transform.Matrix,
            comparer);
    }

    [Fact]
    public void ChangeSphereTransformation()
    {
        var s = new Sphere()
        {
            Transform = new Transform(
                Matrix4x4.CreateTranslation(2, 3, 4)),
        };
        var comparer = new Matrix4x4EqualityComparer(1e-6);
        Assert.Equal(
            Matrix4x4.CreateTranslation(2, 3, 4),
            s.Transform.Matrix,
            comparer);
    }

    [Fact]
    public void IntersectScaledSphereWithRay()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 0, -5),
            Vector4.CreateDirection(0, 0, 1));
        var s = new Sphere()
        {
            Transform = new Transform(
                Matrix4x4.CreateScale(2, 2,2)),
        };
        var xs = s
            .IntersectAll(r)
            .Order()
            .ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(3, xs[0].T);
        Assert.Equal(7, xs[1].T);
    }

    [Fact]
    public void IntersectTranslatedSphereWithRay()
    {
        var r = new Ray(
            Vector4.CreatePosition(0, 0, -5),
            Vector4.CreateDirection(0, 0, 1));
        var s = new Sphere()
        {
            Transform = new Transform(
                Matrix4x4.CreateTranslation(5, 0, 0)), 
        };
        var xs = s
            .IntersectAll(r)
            .Order()
            .ToList();
        Assert.Empty(xs);
    }
}