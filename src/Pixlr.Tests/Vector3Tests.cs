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
}