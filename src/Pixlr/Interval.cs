namespace Pixlr;

public record Interval(double Min, double Max)
{
    public double Clamp(double v) => Math.Clamp(v, this.Min, this.Max);
}