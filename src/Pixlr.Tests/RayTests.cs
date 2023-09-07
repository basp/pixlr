namespace Pixlr.Tests;

public class RayTests
{
    [Fact]
    public void ComputePointFromDistance()
    {
        var r = new Ray(
            Vector4.CreatePosition(2, 3, 4),
            Vector4.CreateDirection(1, 0, 0));
        Assert.Equal(
            Vector4.CreatePosition(2, 3, 4),
            r.GetPoint(0));
        Assert.Equal(
            Vector4.CreatePosition(3, 3, 4),
            r.GetPoint(1));
        Assert.Equal(
            Vector4.CreatePosition(1, 3, 4),
            r.GetPoint(-1));
        Assert.Equal(
            Vector4.CreatePosition(4.5, 3, 4),
            r.GetPoint(2.5));
    }

    [Fact]
    public void TranslateRay()
    {
        var r = new Ray(
            Vector4.CreatePosition(1, 2, 3),
            Vector4.CreateDirection(0, 1, 0));
        var m = Matrix4x4.CreateTranslation(3, 4, 5);
        var r2 = Ray.Transform(r, m);
        Assert.Equal(
            Vector4.CreatePosition(4, 6, 8),
            r2.Origin);
        Assert.Equal(
            Vector4.CreateDirection(0, 1, 0),
            r2.Direction);
    }

    [Fact]
    public void ScaleRay()
    {
        var r = new Ray(
            Vector4.CreatePosition(1, 2, 3),
            Vector4.CreateDirection(0, 1, 0));
        var m = Matrix4x4.CreateScale(2, 3, 4);
        var r2 = Ray.Transform(r, m);
        Assert.Equal(
            Vector4.CreatePosition(2, 6, 12),
            r2.Origin);
        Assert.Equal(
            Vector4.CreateDirection(0, 3, 0),
            r2.Direction);
    }
}