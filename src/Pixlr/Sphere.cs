namespace Pixlr;

public class Sphere : IShape
{
    public Transform Transform { get; init; } = new(Matrix4x4.Identity);

    public IEnumerable<Intersection> IntersectAll(Ray ray)
    {
        ray = Ray.Transform(ray, this.Transform.Inverse);
        
        var OC = ray.Origin - Vector4.CreatePosition(0, 0, 0);
        var D = ray.Direction;
        
        var a = Vector4.Dot(D, D);
        var b = 2 * Vector4.Dot(D, OC);
        var c = Vector4.Dot(OC, OC) - 1;

        var d = b * b - 4 * a * c;
        if (d < 0)
        {
            return Array.Empty<Intersection>();
        }

        var t1 = (-b - Math.Sqrt(d)) / (2 * a);
        var t2 = (-b + Math.Sqrt(d)) / (2 * a);

        var ix1 = new Intersection(t1, this);
        var ix2 = new Intersection(t2, this);
        return new[] { ix1, ix2 };
    }
}