namespace Pixlr;

public record Vector2(double X, double Y)
{
    public static Vector2 Abs(Vector2 v) =>
        new(
            Math.Abs(v.X),
            Math.Abs(v.Y));

    public static Vector2 Add(Vector2 v, Vector2 w) =>
        new(
            v.X + w.X,
            v.Y + w.Y);

    public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max) =>
        new(
            Math.Clamp(v.X, min.X, max.X),
            Math.Clamp(v.Y, min.Y, max.Y));

    public static Vector2 Divide(Vector2 v, double c) =>
        new(
            v.X / c,
            v.Y / c);

    public static Vector2 Divide(Vector2 v, Vector2 w) =>
        new(
            v.X / w.X,
            v.Y / w.Y);
    
    public static double Length(Vector2 v) =>
        Math.Sqrt(LengthSquared(v));

    public static double LengthSquared(Vector2 v) =>
        v.X * v.X +
        v.Y * v.Y;
}