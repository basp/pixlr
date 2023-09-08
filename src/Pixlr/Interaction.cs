namespace Pixlr;

public class Interaction
{   
    public double T { get; init; }
    
    public IShape Object { get; init; }
    
    public Vector4 Point { get; init; }
    
    public Vector4 Eye { get; init; }
    
    public Vector4 Normal { get; init; }
    
    public bool Inside { get; init; }

    public static Interaction FromIntersection(
        Intersection intersection,
        Ray ray)
    {
        var point = ray.GetPosition(intersection.T);
        var eye = Vector4.Negate(ray.Direction);
        var normal = intersection.Obj.GetNormal(point);
        var inside = Vector4.Dot(normal, eye) < 0;
        return new Interaction()
        {
            T = intersection.T,
            Object = intersection.Obj,
            Point = point,
            Eye = eye,
            Inside = inside,
            Normal = inside ? Vector4.Negate(normal) : normal,
        };
    }
}