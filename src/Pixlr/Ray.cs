namespace Pixlr;

public record Ray(Vector4 Origin, Vector4 Direction)
{
    public Vector4 GetPosition(double t) =>
        this.Origin + t * this.Direction;

    public static Ray Transform(Ray ray, Matrix4x4 matrix) =>
        new(
            Vector4.Transform(ray.Origin, matrix),
            Vector4.Transform(ray.Direction, matrix));
}