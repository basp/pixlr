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
        var color = intr.Object.Material.GetColor(
            this.Lights.First(),
            intr.Point,
            intr.Eye,
            intr.Normal);
        return color;
    }
}