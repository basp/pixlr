namespace Pixlr;

public interface IShape
{
    Transform Transform { get; init; }
    
    Material Material { get; set; }

    Vector4 GetNormal(Vector4 point);

    IEnumerable<Intersection> IntersectAll(Ray ray);
}