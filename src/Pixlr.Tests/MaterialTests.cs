using System.Numerics;

namespace Pixlr.Tests;

public class MaterialTests
{
    public IMaterial m = new Material();
    public Vector4 position = Vector4.CreatePosition(0, 0, 0);
    
    [Fact]
    public void TheDefaultMaterial()
    {
        var m = new Material();
        Assert.Equal(new Color(1, 1, 1), m.Color);
        Assert.Equal(0.1, m.Ambient);
        Assert.Equal(0.9, m.Diffuse);
        Assert.Equal(0.9, m.Specular);
        Assert.Equal(200, m.Shininess);
    }

    [Fact]
    public void LightingWithEyeBetweenLightAndSurface()
    {
        var eyev = Vector4.CreateDirection(0, 0, -1);
        var normalv = Vector4.CreateDirection(0, 0, -1);
        var light = new PointLight(
            Vector4.CreatePosition(0, 0, -10),
            new Color(1, 1, 1));
        var actual = this.m.GetColor(light, this.position, eyev, normalv);
        var expected = new Color(1.9, 1.9, 1.9);
        var comparer = new ColorEqualityComparer();
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void LightingWithEyeBetweenLightAndSurfaceAndEyeOffsetBy45Degrees()
    {
        var eyev = Vector4.CreateDirection(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
        var normalv = Vector4.CreateDirection(0, 0, -1);
        var light = new PointLight(
            Vector4.CreatePosition(0, 0, -10),
            new Color(1, 1, 1));
        var actual = this.m.GetColor(light, this.position, eyev, normalv);
        var expected = new Color(1, 1, 1);
        var comparer = new ColorEqualityComparer();
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void LightingWithEyeOppositeSurfaceAndLightOffset45Degrees()
    {
        var eyev = Vector4.CreateDirection(0, 0, -1);
        var normalv = Vector4.CreateDirection(0, 0, -1);
        var light = new PointLight(
            Vector4.CreatePosition(0, 10, -10),
            new Color(1, 1, 1));
        var actual = this.m.GetColor(light, this.position, eyev, normalv);
        var expected = new Color(0.7364, 0.7364, 0.7364);
        var comparer = new ColorEqualityComparer(1e-5);
        Assert.Equal(expected, actual, comparer);
    }
}