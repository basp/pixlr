namespace Pixlr;

public interface IShape
{
    Transform Transform { get; init; }

    Vector4 GetNormal(Vector4 point);

    IEnumerable<Intersection> IntersectAll(Ray ray);
}