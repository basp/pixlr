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
}