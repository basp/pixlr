using System.Numerics;
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

    [Fact]
    public void NormalAtPointOnAxis()
    {
        var s = new Sphere();
        
        var points = new[]
        {
            (1, 0, 0),
            (0, 1, 0),
            (0, 0, 1),
            (Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3),
        };

        var tests = points
            .Select(p =>
            {
                var (x, y, z) = p;
                return new
                {
                    Point = Vector4.CreatePosition(x, y, z),
                    Expected = Vector4.CreateDirection(x, y, z),
                };
            });

        foreach (var test in tests)
        {
            var actual = s.GetNormal(test.Point);
            Assert.Equal(test.Expected, actual);
            // The normal should be a normalized vector.
            Assert.Equal(
                1.0, 
                Vector4.Length(actual), 
                tolerance:1e-6);
        }
    }

    [Fact]
    public void ComputeTheNormalOnATranslatedSphere()
    {
        var m = Matrix4x4
            .Identity
            .Translate(0, 1, 0);
        var s = new Sphere()
        {
            Transform = new Transform(m),
        };
        var p = Vector4.CreatePosition(0, 1.70711, -0.70711);
        var actual = s.GetNormal(p);
        var expected = Vector4.CreateDirection(0, 0.70711, -0.70711);
        var comparer = new Vector4EqualityComparer(1e-5);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void ComputeTheNormalOnATransformedSphere()
    {
        var m = Matrix4x4
            .Identity
            .RotateZ(Math.PI / 5)
            .Scale(1, 0.5, 1);
        var s = new Sphere()
        {
            Transform = new Transform(m),
        };
        var sqrt2over2 = Math.Sqrt(2) / 2;
        var p = Vector4.CreatePosition(0, sqrt2over2, -sqrt2over2);
        var actual = s.GetNormal(p);
        var expected = Vector4.CreateDirection(0, 0.97014, -0.24254);
        var comparer = new Vector4EqualityComparer(1e-5);
        Assert.Equal(expected, actual, comparer);
    }
}