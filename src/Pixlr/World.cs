using System.Xml.Xsl;

namespace Pixlr;

public class World : IPrimitive
{
    public IList<IShape> Objects { get; init; } =
        new List<IShape>();

    public IList<PointLight> Lights { get; init; } =
        new List<PointLight>();

    public IEnumerable<Intersection> IntersectAll(Ray ray) =>
        this.Objects.SelectMany(obj => obj.IntersectAll(ray));

    public Color GetColor(Ray ray)
    {
        var ix = IntersectAll(ray);
        if (ix.TryGetHit(out var i))
        {
            var intr = Interaction.FromIntersection(i, ray);
            return this.GetColor(intr);
        }

        return new Color(0, 0, 0);
    }
    
    public Color GetColor(Interaction intr)
    {
        // Supporting multiple light sources would
        // mean iterating over each light and adding the
        // colors together.
        var light = this.Lights.First();
        var color = intr.Object.Material.Lighting(light, intr); 
        // var color = intr.Object.Material.GetColor(
        //     this.Lights.First(),
        //     intr.Point,
        //     intr.Eye,
        //     intr.Normal);
        return color;
    }

    public bool TestShadow(Vector4 point)
    {
        var light = this.Lights.First();
        var v = light.Position - point;
        var distance = Vector4.Length(v);
        var direction = Vector4.Normalize(v);
        var r = new Ray(point, direction);
        var xs = this.IntersectAll(r);
        if (xs.TryGetHit(out var ix))
        {
            if (ix.T < distance)
            {
                return true;
            }
        }

        return false;
    }
}