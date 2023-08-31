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
}