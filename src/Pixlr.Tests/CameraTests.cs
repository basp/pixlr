namespace Pixlr.Tests;

public class CameraTests
{
    [Fact]
    public void CameraConstruction()
    {
        var (width, height) = (160, 120);
        const double fov = Math.PI / 2;
        var cam = new Camera(width, height, fov);
        var comparer = new Matrix4x4EqualityComparer(1e-6);
        Assert.Equal((160, 120), cam.Resolution);
        Assert.Equal(Math.PI / 2, fov);
        Assert.Equal(
            Matrix4x4.Identity,
            cam.Transform.Matrix,
            comparer);
    }

    [Fact]
    public void ThePixelSizeForAHorizontalCanvas()
    {
        var cam = new Camera(200, 125, Math.PI / 2);
        Assert.Equal(0.01, cam.PixelSize, tolerance: 1e-6);
    }

    [Fact]
    public void ThePixelSizeForAVerticalCanvas()
    {
        var cam = new Camera(125, 200, Math.PI / 2);
        Assert.Equal(0.01, cam.PixelSize, tolerance: 1e-6);
    }

    [Fact]
    public void ConstructRayThroughCenterOfTheCanvas()
    {
        var cam = new Camera(201, 101, Math.PI / 2);
        var r = cam.GenerateRay(100, 50);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(
            Vector4.CreatePosition(0, 0, 0),
            r.Origin,
            comparer);
        Assert.Equal(
            Vector4.CreateDirection(0, 0, -1),
            r.Direction,
            comparer);
    }

    [Fact]
    public void ConstructRayThroughCornerOfTheCanvas()
    {
        var cam = new Camera(201, 101, Math.PI / 2);
        var r = cam.GenerateRay(0, 0);
        var comparer = new Vector4EqualityComparer(1e-5);
        Assert.Equal(
            Vector4.CreatePosition(0, 0, 0),
            r.Origin,
            comparer);
        Assert.Equal(
            Vector4.CreateDirection(0.66519, 0.33259, -0.66851),
            r.Direction,
            comparer);
    }

    [Fact]
    public void ConstructRayWhenCameraIsTransformed()
    {
        var cam = new Camera(201, 101, Math.PI / 2)
        {
            Transform = new Transform(
                Matrix4x4.Multiply(
                    Matrix4x4.CreateRotationY(Math.PI / 4),
                    Matrix4x4.CreateTranslation(0, -2, 5))),
        };
        var r = cam.GenerateRay(100, 50);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(
            Vector4.CreatePosition(0, 2, -5),
            r.Origin,
            comparer);
        Assert.Equal(
            Vector4.CreateDirection(Math.Sqrt(2) / 2, 0, -Math.Sqrt(2) / 2),
            r.Direction,
            comparer);
    }
}