namespace Pixlr.Tests;

public class Vector2Tests
{
    [Fact]
    public void TestAbsoluteValue()
    {
        var v = new Vector2(1.2, -0.3);
        var expected = new Vector2(1.2, 0.3);
        Assert.Equal(expected, Vector2.Abs(v));
    }

    [Fact]
    public void TestVectorAddition()
    {
        var v = new Vector2(1.3, 1.2);
        var w = new Vector2(-0.3, -0.2);
        var expected = new Vector2(1.0, 1.0);
        Assert.Equal(expected, Vector2.Add(v, w));
    }

    [Fact]
    public void TestClamp()
    {
        var v = new Vector2(-1.0, 1.5);
        var min = new Vector2(0, 0);
        var max = new Vector2(2, 2);
        var expected = new Vector2(0, 1.5);
        Assert.Equal(expected, Vector2.Clamp(v, min, max));
    }
    
    [Fact]
    public void TestScalarDivision()
    {
        var v = new Vector2(-1.0, 3.0);
        var expected = new Vector2(-0.5, 1.5);
        Assert.Equal(expected, Vector2.Divide(v, 2));
    }

    [Fact]
    public void TestVectorDivision()
    {
        var v = new Vector2(1, 2);
        var w = new Vector2(-1, -2);
        var expected = new Vector2(-1, -1);
        Assert.Equal(expected, Vector2.Divide(v, w));
    }
    
    [Fact]
    public void TestVectorLengthSquared()
    {
        var v = new Vector2(1, 2);
        Assert.Equal(5, Vector2.LengthSquared(v));
    }

    [Fact]
    public void TestVectorLength()
    {
        var v = new Vector2(1, 2);
        Assert.Equal(
            Math.Sqrt(5),
            Vector2.Length(v),
            tolerance: 1e-6);
    }
}