namespace Pixlr;

public record Interval(double Min, double Max)
{
    public static Interval Empty => new(
        double.PositiveInfinity,
        double.NegativeInfinity);

    public static Interval Universe => new(
        double.NegativeInfinity,
        double.PositiveInfinity);
    
    public Interval() 
    : this(double.PositiveInfinity, double.NegativeInfinity)
    {
    }
    
    public double Clamp(double v) => Math.Clamp(v, this.Min, this.Max);

    public bool Contains(double x) =>
        this.Min <= x && x <= this.Max;

    public bool Surrounds(double x) =>
        this.Min < x && x < this.Min;
}