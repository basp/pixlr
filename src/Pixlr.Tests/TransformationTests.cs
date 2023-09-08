namespace Pixlr.Tests;

public class TransformationTests
{
    [Fact]
    public void TheTransformationMatrixForTheDefaultOrientation()
    {
        var from = Vector4.CreatePosition(0, 0, 0);
        var to = Vector4.CreatePosition(0, 0, -1);
        var up = Vector4.CreateDirection(0, 1, 0);
        var actual = Matrix4x4.CreateLookAt(from, to, up);
        var comparer = new Matrix4x4EqualityComparer(1e-6);
        Assert.Equal(Matrix4x4.Identity, actual, comparer);
    }

    [Fact]
    public void ViewTransformationLookingInPositiveZDirection()
    {
        var from = Vector4.CreatePosition(0, 0, 0);
        var to = Vector4.CreatePosition(0, 0, 1);
        var up = Vector4.CreateDirection(0, 1, 0);
        var actual = Matrix4x4.CreateLookAt(from, to, up);
        var expected = Matrix4x4.CreateScale(-1, 1, -1);
        var comparer = new Matrix4x4EqualityComparer(1e-6);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void ViewTransformationMovesTheWorld()
    {
        var from = Vector4.CreatePosition(0, 0, 8);
        var to = Vector4.CreatePosition(0, 0, 0);
        var up = Vector4.CreateDirection(0, 1, 0);
        var actual = Matrix4x4.CreateLookAt(from, to, up);
        var expected = Matrix4x4.CreateTranslation(0, 0, -8);
        var comparer = new Matrix4x4EqualityComparer(1e-6);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void AnArbitraryViewTransformation()
    {
        var from = Vector4.CreatePosition(1, 3, 2);
        var to = Vector4.CreatePosition(4, -2, 8);
        var up = Vector4.CreateDirection(1, 1, 0);
        var actual = Matrix4x4.CreateLookAt(from, to, up);
        var expected = new Matrix4x4(
            -0.50709, 0.50709, 0.67612, -2.36643,
            0.76772, 0.60609, 0.12122, -2.82843,
            -0.35857, 0.59761, -0.71714, 0.00000,
            0.00000, 0.00000, 0.00000, 1.00000);
        var comparer = new Matrix4x4EqualityComparer(1e-5);
        Assert.Equal(expected, actual, comparer);
    }
}