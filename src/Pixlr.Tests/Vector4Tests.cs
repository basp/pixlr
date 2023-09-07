namespace Pixlr.Tests;

public class Vector4Tests
{
    [Fact]
    public void TestAbsoluteValue()
    {
        var v = new Vector4(-0.5, 0.5, 1.0, -1.0);
        var expected = new Vector4(0.5, 0.5, 1.0, 1.0);
        Assert.Equal(expected, Vector4.Abs(v));
    }

    [Fact]
    public void TestVectorAddition()
    {
        var v = new Vector4(-0.5, 0.5, -1.0, 1.5);
        var w = new Vector4(1.0, -0.3, -0.2, 0.5);
        var expected = new Vector4(0.5, 0.2, -1.2, 2.0);
        Assert.Equal(expected, Vector4.Add(v, w));
    }

    [Fact]
    public void TestClamp()
    {
        var v = new Vector4(-1.0, 1.5, 2.3, 1.2);
        var min = new Vector4(0, 0, 0, 0);
        var max = new Vector4(2, 2, 2, 2);
        var expected = new Vector4(0, 1.5, 2, 1.2);
        Assert.Equal(expected, Vector4.Clamp(v, min, max));
    }

    [Fact]
    public void TestScalarDivision()
    {
        var v = new Vector4(-1.0, 3.0, 2.0, 4);
        var expected = new Vector4(-0.5, 1.5, 1.0, 2);
        Assert.Equal(expected, Vector4.Divide(v, 2));
    }

    [Fact]
    public void TestVectorDivision()
    {
        var v = new Vector4(1, 2, 3, 4);
        var w = new Vector4(-1, -2, -3, -4);
        var expected = new Vector4(-1, -1, -1, -1);
        Assert.Equal(expected, Vector4.Divide(v, w));
    }

    [Fact]
    public void TestDotProduct()
    {
        var u = Vector4.CreateDirection(1, 2, 3);
        var v = Vector4.CreateDirection(2, 3, 4);
        Assert.Equal(20, Vector4.Dot(u, v));
    }

    [Fact]
    public void TestVectorLengthSquared()
    {
        var v = new Vector4(1, 2, 3, 4);
        Assert.Equal(30, Vector4.LengthSquared(v));
    }

    [Fact]
    public void TestVectorLength()
    {
        var v = new Vector4(1, 2, 3, 4);
        Assert.Equal(
            Math.Sqrt(30),
            Vector4.Length(v),
            tolerance: 1e-6);
    }

    [Fact]
    public void TestElementMax()
    {
        var u = new Vector4(1, -2, 3, -4);
        var v = new Vector4(-1, 2, -3, 4);
        var actual = Vector4.Max(u, v);
        var expected = new Vector4(1, 2, 3, 4);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestElementMin()
    {
        var u = new Vector4(1, -2, 3, -4);
        var v = new Vector4(-1, 2, -3, 4);
        var actual = Vector4.Min(u, v);
        var expected = new Vector4(-1, -2, -3, -4);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestScalarMultiplication()
    {
        var u = new Vector4(1, 2, 3, 4);
        var expected = new Vector4(0.5, 1, 1.5, 2);
        Assert.Equal(expected, Vector4.Multiply(u, 0.5));
        Assert.Equal(expected, Vector4.Multiply(0.5, u));
    }

    [Fact]
    public void TestTransform()
    {
        var a = new Matrix4x4(
            1, 2, 3, 4,
            2, 4, 4, 2,
            8, 6, 4, 1,
            0, 0, 0, 1);

        var u = new Vector4(1, 2, 3, 1);

        var expected = new Vector4(18, 24, 33, 1);
        var comparer = new Vector4EqualityComparer(1e-6);
        var actual = Vector4.Transform(u, a);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void TransformByIdentityMatrix()
    {
        var u = new Vector4(1, 2, 3, 4);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(u, Vector4.Transform(u, Matrix4x4.Identity), comparer);
    }

    [Fact]
    public void TransformByTranslationMatrix()
    {
        var transform = Matrix4x4.CreateTranslation(5, -3, 2);
        var p = Vector4.CreatePosition(-3, 4, 5);
        var expected = Vector4.CreatePosition(2, 1, 7);
        var actual = Vector4.Transform(p, transform);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void TransformByInverseOfTranslationMatrix()
    {
        var transform = Matrix4x4.CreateTranslation(5, -3, 2);
        Assert.True(Matrix4x4.Invert(transform, out var inv));
        var p = Vector4.CreatePosition(-3, 4, 5);
        var expected = Vector4.CreatePosition(-8, 7, 3);
        var actual = Vector4.Transform(p, inv);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void TranslationDoesNotAffectDirections()
    {
        var transform = Matrix4x4.CreateTranslation(5, -3, 2);
        var v = Vector4.CreateDirection(-3, 4, 5);
        Assert.Equal(v, Vector4.Transform(v, transform));
    }

    [Fact]
    public void ScalingAppliedToPosition()
    {
        var transform = Matrix4x4.CreateScale(2, 3, 4);
        var p = Vector4.CreatePosition(-4, 6, 8);
        var expected = Vector4.CreatePosition(-8, 18, 32);
        var actual = Vector4.Transform(p, transform);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void ScalingAppliedToDirection()
    {
        var transform = Matrix4x4.CreateScale(2, 3, 4);
        var v = Vector4.CreateDirection(-4, 6, 8);
        var expected = Vector4.CreateDirection(-8, 18, 32);
        var actual = Vector4.Transform(v, transform);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void TransformByInverseOfScalingMatrix()
    {
        var transform = Matrix4x4.CreateScale(2, 3, 4);
        Assert.True(Matrix4x4.Invert(transform, out var inv));
        var v = Vector4.CreateDirection(-4, 6, 8);
        var expected = Vector4.CreateDirection(-2, 2, 2);
        var actual = Vector4.Transform(v, inv);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void ReflectionIsScalingByNegativeValue()
    {
        var transform = Matrix4x4.CreateScale(-1, 1, 1);
        var p = Vector4.CreatePosition(2, 3, 4);
        var expected = Vector4.CreatePosition(-2, 3, 4);
        var actual = Vector4.Transform(p, transform);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RotatePointAroundXAxis()
    {
        var p = Vector4.CreatePosition(0, 1, 0);
        var halfQuarter = Matrix4x4.CreateRotationX(Math.PI / 4);
        var fullQuarter = Matrix4x4.CreateRotationX(Math.PI / 2);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(
            Vector4.CreatePosition(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2),
            Vector4.Transform(p, halfQuarter),
            comparer);
        Assert.Equal(
            Vector4.CreatePosition(0, 0, 1),
            Vector4.Transform(p, fullQuarter),
            comparer);
    }

    [Fact]
    public void InverseXRotationRotatesInOppositeDirection()
    {
        var p = Vector4.CreatePosition(0, 1, 0);
        var halfQuarter = Matrix4x4.CreateRotationX(Math.PI / 4);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.True(Matrix4x4.Invert(halfQuarter, out var inv));
        Assert.Equal(
            Vector4.CreatePosition(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2),
            Vector4.Transform(p, inv),
            comparer);
    }

    [Fact]
    public void RotatePointAroundYAxis()
    {
        var p = Vector4.CreatePosition(0, 0, 1);
        var halfQuarter = Matrix4x4.CreateRotationY(Math.PI / 4);
        var fullQuarter = Matrix4x4.CreateRotationY(Math.PI / 2);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(
            Vector4.CreatePosition(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2),
            Vector4.Transform(p, halfQuarter),
            comparer);
        Assert.Equal(
            Vector4.CreatePosition(1, 0, 0),
            Vector4.Transform(p, fullQuarter),
            comparer);
    }

    [Fact]
    public void RotatePointAroundZAxis()
    {
        var p = Vector4.CreatePosition(0, 1, 0);
        var halfQuarter = Matrix4x4.CreateRotationZ(Math.PI / 4);
        var fullQuarter = Matrix4x4.CreateRotationZ(Math.PI / 2);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(
            Vector4.CreatePosition(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0),
            Vector4.Transform(p, halfQuarter),
            comparer);
        Assert.Equal(
            Vector4.CreatePosition(-1, 0, 0),
            Vector4.Transform(p, fullQuarter),
            comparer);
    }

    [Fact]
    public void ShearingMovesXInProportionToY()
    {
        var transform = Matrix4x4.CreateShear(1, 0, 0, 0, 0, 0);
        var p = Vector4.CreatePosition(2, 3, 4);
        var expected = Vector4.CreatePosition(5, 3, 4);
        var actual = Vector4.Transform(p, transform);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShearingMovesXInProportionToZ()
    {
        var transform = Matrix4x4.CreateShear(0, 1, 0, 0, 0, 0);
        var p = Vector4.CreatePosition(2, 3, 4);
        var expected = Vector4.CreatePosition(6, 3, 4);
        var actual = Vector4.Transform(p, transform);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShearingMovesYInProportionToX()
    {
        var transform = Matrix4x4.CreateShear(0, 0, 1, 0, 0, 0);
        var p = Vector4.CreatePosition(2, 3, 4);
        var expected = Vector4.CreatePosition(2, 5, 4);
        var actual = Vector4.Transform(p, transform);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShearingMovesYInProportionToZ()
    {
        var transform = Matrix4x4.CreateShear(0, 0, 0, 1, 0, 0);
        var p = Vector4.CreatePosition(2, 3, 4);
        var expected = Vector4.CreatePosition(2, 7, 4);
        var actual = Vector4.Transform(p, transform);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShearingMovesZInProportionToX()
    {
        var transform = Matrix4x4.CreateShear(0, 0, 0, 0, 1, 0);
        var p = Vector4.CreatePosition(2, 3, 4);
        var expected = Vector4.CreatePosition(2, 3, 6);
        var actual = Vector4.Transform(p, transform);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShearingMovesZInProportionToY()
    {
        var transform = Matrix4x4.CreateShear(0, 0, 0, 0, 0, 1);
        var p = Vector4.CreatePosition(2, 3, 4);
        var expected = Vector4.CreatePosition(2, 3, 7);
        var actual = Vector4.Transform(p, transform);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IndividualTransformationsAreAppliedInSequence()
    {
        var A = Matrix4x4.CreateRotationX(Math.PI / 2);
        var B = Matrix4x4.CreateScale(5, 5, 5);
        var C = Matrix4x4.CreateTranslation(10, 5, 7);

        var p1 = Vector4.CreatePosition(1, 0, 1);
        var p2 = Vector4.Transform(p1, A);
        var p3 = Vector4.Transform(p2, B);
        var p4 = Vector4.Transform(p3, C);

        var comparer = new Vector4EqualityComparer(1e-6);

        Assert.Equal(
            Vector4.CreatePosition(1, -1, 0),
            p2,
            comparer);
        Assert.Equal(
            Vector4.CreatePosition(5, -5, 0),
            p3,
            comparer);
        Assert.Equal(
            Vector4.CreatePosition(15, 0, 7),
            p4,
            comparer);
    }

    [Fact]
    public void ChainedTransformationsMustBeAppliedInReverse()
    {
        var p = Vector4.CreatePosition(1, 0, 1);
        var A = Matrix4x4.CreateRotationX(Math.PI / 2);
        var B = Matrix4x4.CreateScale(5, 5, 5);
        var C = Matrix4x4.CreateTranslation(10, 5, 7);
        var T = C * B * A;
        var expected = Vector4.CreatePosition(15, 0, 7);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(expected, Vector4.Transform(p, T), comparer);
    }

    [Fact]
    public void FluentApiTransformationsAreAppliedInOrder()
    {
        var p = Vector4.CreatePosition(1, 0, 1);
        var T = Matrix4x4
            .Identity
            .RotateX(Math.PI / 2)
            .Scale(5, 5, 5)
            .Translate(10, 5, 7);
        var expected = Vector4.CreatePosition(15, 0, 7);
        var comparer = new Vector4EqualityComparer(1e-6);
        Assert.Equal(expected, Vector4.Transform(p, T), comparer);
    }
}