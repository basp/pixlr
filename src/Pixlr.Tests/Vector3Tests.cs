namespace Pixlr.Tests;

public class Vector3Tests
{
    [Fact]
    public void AbsoluteValue()
    {
        var v = new Vector3(-1.3, 0.2, -0.4);
        var expected = new Vector3(1.3, 0.2, 0.4);
        Assert.Equal(expected, Vector3.Abs(v));
    }
    
    [Fact]
    public void VectorAddition()
    {
        var v = new Vector3(1.3, 1.2, 1.1);
        var w = new Vector3(-0.3, -0.2, 0.4);
        var expected = new Vector3(1.0, 1.0, 1.5);
        Assert.Equal(expected, Vector3.Add(v, w));
    }
    
    [Fact]
    public void TestClamp()
    {
        var v = new Vector3(-1.0, 1.5, 2.3);
        var min = new Vector3(0, 0, 0);
        var max = new Vector3(2, 2, 2);
        var expected = new Vector3(0, 1.5, 2);
        Assert.Equal(expected, Vector3.Clamp(v, min, max));
    }
    
    [Fact]
    public void TestScalarDivision()
    {
        var v = new Vector3(-1.0, 3.0, 2.0);
        var expected = new Vector3(-0.5, 1.5, 1.0);
        Assert.Equal(expected, Vector3.Divide(v, 2));
    }

    [Fact]
    public void TestVectorDivision()
    {
        var v = new Vector3(1, 2, 3);
        var w = new Vector3(-1, -2, -3);
        var expected = new Vector3(-1, -1, -1);
        Assert.Equal(expected, Vector3.Divide(v, w));
    }   
    
    [Fact]
    public void TestVectorLengthSquared()
    {
        var v = new Vector3(1, 2, 3);
        Assert.Equal(14, Vector3.LengthSquared(v));
    }

    [Fact]
    public void TestVectorLength()
    {
        var v = new Vector3(1, 2, 3);
        Assert.Equal(
            Math.Sqrt(14),
            Vector3.Length(v),
            tolerance: 1e-6);
    }

    [Fact]
    public void TestCrossProduct()
    {
        var a = new Vector3(1, 2, 3);
        var b = new Vector3(2, 3, 4);
        Assert.Equal(
            new Vector3(-1, 2, -1),
            Vector3.Cross(a, b));
        Assert.Equal(
            new Vector3(1, -2, 1),
            Vector3.Cross(b, a));
    }
}