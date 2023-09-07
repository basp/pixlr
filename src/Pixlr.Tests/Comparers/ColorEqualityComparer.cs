namespace Pixlr.Tests.Comparers;

internal class ColorEqualityComparer : ApproxEqualityComparer<Color>
{
    public ColorEqualityComparer(double atol = 1e-6)
        : base(atol)
    {
    }
    
    public override bool Equals(Color x, Color y) =>
        this.ApproxEqual(x.R, y.R) &&
        this.ApproxEqual(x.G, y.G) &&
        this.ApproxEqual(x.B, y.B);

    public override int GetHashCode(Color obj) => obj.GetHashCode();
}