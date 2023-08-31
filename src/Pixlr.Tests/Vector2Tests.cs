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
    }
}