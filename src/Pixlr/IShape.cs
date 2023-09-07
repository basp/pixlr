namespace Pixlr;

public interface IShape
{
    Transform Transform { get; init; }
    
    IEnumerable<Intersection> IntersectAll(Ray ray);
}