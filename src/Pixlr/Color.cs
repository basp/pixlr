namespace Pixlr;

public record struct Color(double R, double G, double B)
{
    public static Color Add(Color c1, Color c2) =>
        new(
            c1.R + c2.R,
            c1.G + c2.G,
            c1.B + c2.B);

    public static Color Subtract(Color c1, Color c2) =>
        new(
            c1.R - c2.R,
            c1.G - c2.G,
            c1.B - c2.B);

    public static Color Multiply(Color c, double s) =>
        new(
            c.R * s,
            c.G * s,
            c.B * s);

    public static Color Multiply(double s, Color c) =>
        new(
            s * c.R,
            s * c.G,
            s * c.B);
    
    public static Color Multiply(Color c1, Color c2) =>
        new(
            c1.R * c2.R,
            c1.G * c2.G,
            c1.B * c2.B);
}