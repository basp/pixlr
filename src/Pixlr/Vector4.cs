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

    public static Vector4 CreateDirection(double x, double y, double z) =>
        new(x, y, z, 0);

    public static Vector4 CreatePosition(double x, double y, double z) =>
        new(x, y, z, 1);

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

    public static Vector4 Max(Vector4 u, Vector4 v) =>
        new(
            Math.Max(u.X, v.X),
            Math.Max(u.Y, v.Y),
            Math.Max(u.Z, v.Z),
            Math.Max(u.W, v.W));

    public static Vector4 Min(Vector4 u, Vector4 v) =>
        new(
            Math.Min(u.X, v.X),
            Math.Min(u.Y, v.Y),
            Math.Min(u.Z, v.Z),
            Math.Min(u.W, v.W));

    public static Vector4 Multiply(double s, Vector4 u) =>
        new(
            s * u.X,
            s * u.Y,
            s * u.Z,
            s * u.W);

    public static Vector4 Multiply(Vector4 u, double s) =>
        new(
            u.X * s,
            u.Y * s,
            u.Z * s,
            u.W * s);

    public static Vector4 Negate(Vector4 u) =>
        new(
            -u.X,
            -u.Y,
            -u.Z,
            -u.W);

    public static Vector4 Normalize(Vector4 u)
    {
        var s = 1.0 / Length(u);
        return u * s;
    }

    public static Vector4 Subtract(Vector4 u, Vector4 v) =>
        new(
            u.X - v.X,
            u.Y - v.Y,
            u.Z - v.Z,
            u.W - v.W);

    /// <summary>
    /// Transforms a vector.
    /// </summary>
    /// <param name="vector">The vector to transform.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>
    /// <see cref="Vector4"/>
    /// </returns>
    public static Vector4 Transform(Vector4 vector, Matrix4x4 matrix)
    {
        var x =
            (matrix[0, 0] * vector.X) +
            (matrix[0, 1] * vector.Y) +
            (matrix[0, 2] * vector.Z) +
            (matrix[0, 3] * vector.W);
        var y =
            (matrix[1, 0] * vector.X) +
            (matrix[1, 1] * vector.Y) +
            (matrix[1, 2] * vector.Z) +
            (matrix[1, 3] * vector.W);
        var z =
            (matrix[2, 0] * vector.X) +
            (matrix[2, 1] * vector.Y) +
            (matrix[2, 2] * vector.Z) +
            (matrix[2, 3] * vector.W);
        var w =
            (matrix[3, 0] * vector.X) +
            (matrix[3, 1] * vector.Y) +
            (matrix[3, 2] * vector.Z) +
            (matrix[3, 3] * vector.W);
        return new Vector4(x, y, z, w);
    }

    public static Vector4 operator +(Vector4 u, Vector4 v) =>
        Add(u, v);

    public static Vector4 operator -(Vector4 u, Vector4 v) =>
        Subtract(u, v);

    public static Vector4 operator *(Vector4 u, double s) =>
        Multiply(u, s);

    public static Vector4 operator *(double s, Vector4 u) =>
        Multiply(s, u);
}