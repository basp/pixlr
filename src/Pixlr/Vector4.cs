namespace Pixlr;

public record Vector4(double X, double Y, double Z, double W)
{
    public static Vector4 Abs(Vector4 v) =>
        new(
            Math.Abs(v.X),
            Math.Abs(v.Y),
            Math.Abs(v.Z),
            Math.Abs(v.W));

    public static Vector4 Add(Vector4 v, Vector4 w) =>
        new(
            v.X + w.X,
            v.Y + w.Y,
            v.Z + w.Z,
            v.W + w.W);

    public static Vector4 Clamp(Vector4 v, Vector4 min, Vector4 max) =>
        new(
            Math.Clamp(v.X, min.X, max.X),
            Math.Clamp(v.Y, min.Y, max.Y),
            Math.Clamp(v.Z, min.Z, max.Z),
            Math.Clamp(v.W, min.W, max.W));

    public static Vector4 Divide(Vector4 v, double c) =>
        new(
            v.X / c,
            v.Y / c,
            v.Z / c,
            v.W / c);

    public static Vector4 Divide(Vector4 v, Vector4 w) =>
        new(
            v.X / w.X,
            v.Y / w.Y,
            v.Z / w.Z,
            v.W / w.W);

    public static double Dot(Vector4 v, Vector4 w) =>
        v.X * w.X +
        v.Y * w.Y +
        v.Z * w.Z +
        v.W * w.W;

    public static double Length(Vector4 v) =>
        Math.Sqrt(LengthSquared(v));
    
    public static double LengthSquared(Vector4 v) =>
        v.X * v.X +
        v.Y * v.Y +
        v.Z * v.Z +
        v.W * v.W;
}