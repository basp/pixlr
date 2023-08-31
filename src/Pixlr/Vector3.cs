namespace Pixlr;

public record Vector3(double X, double Y, double Z)
{
    public static Vector3 Abs(Vector3 v) =>
        new(
            Math.Abs(v.X),
            Math.Abs(v.Y),
            Math.Abs(v.Z));
    
    public static Vector3 Add(Vector3 v, Vector3 w) =>
        new(
            v.X + w.X,
            v.Y + w.Y,
            v.Z + w.Z);

    public static Vector3 Clamp(Vector3 v, Vector3 min, Vector3 max) =>
        new(
            Math.Clamp(v.X, min.X, max.X),
            Math.Clamp(v.Y, min.Y, max.Y),
            Math.Clamp(v.Z, min.Z, max.Z));

    public static Vector3 Cross(Vector3 v, Vector3 w) =>
        throw new NotImplementedException();
    
    public static Vector3 Divide(Vector3 v, double c) =>
        new(
            v.X / c,
            v.Y / c,
            v.Z / c);

    public static Vector3 Divide(Vector3 v, Vector3 w) =>
        new(
            v.X / w.X,
            v.Y / w.Y,
            v.Z / w.Z);
    
    public static double Length(Vector3 v) =>
        Math.Sqrt(LengthSquared(v));

    public static double LengthSquared(Vector3 v) =>
        v.X * v.X +
        v.Y * v.Y +
        v.Z * v.Z;
}