namespace Pixlr.Tests;

public class ColorTests
{
    [Fact]
    public void AddColors()
    {
        var c1 = new Color(0.2, 0.2, 0.2);
        var c2 = new Color(0.3, 0.4, 0.5);
        var expected = new Color(0.5, 0.6, 0.7);
        var comparer = new ColorEqualityComparer();
        Assert.Equal(expected, Color.Add(c1, c2), comparer);
    }

    [Fact]
    public void SubtractColors()
    {
        // Colors can be negative during ray tracing calculations;
        // They will be clamped when written to the pixmap.
        var c1 = new Color(0.2, 0.2, 0.2);
        var c2 = new Color(0.3, 0.4, 0.5);
        var expected = new Color(-0.1, -0.2, -0.3);
        var comparer = new ColorEqualityComparer();
        Assert.Equal(expected, Color.Subtract(c1, c2), comparer);
    }

    [Fact]
    public void HadamardProduct()
    {
        var c1 = new Color(0.5, 1.0, 2.0);
        var c2 = new Color(2.0, 1.0, 0.5);
        var expected = new Color(1.0, 1.0, 1.0);
        var comparer = new ColorEqualityComparer();
        Assert.Equal(expected, Color.Multiply(c1, c2), comparer);
    }

    [Fact]
    public void ScaleColor()
    {
        var c = new Color(0.5, 0.5, 0.5);
        var expected = new Color(1, 1, 1);
        Assert.Equal(expected, Color.Multiply(2, c));
        Assert.Equal(expected, Color.Multiply(c, 2));
    }
}