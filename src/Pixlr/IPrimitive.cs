namespace Pixlr;

public interface IPrimitive
{
    IEnumerable<Intersection> IntersectAll(Ray ray);
}