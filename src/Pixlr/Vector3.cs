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
}