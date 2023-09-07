namespace Pixlr;

public record Intersection(double T, IShape Obj)
    : IComparable<Intersection>
{
    public int CompareTo(Intersection other) => this.T.CompareTo(other.T);
}

public static class IntersectionExtensions
{
    public static bool TryGetHit(
        this IEnumerable<Intersection> self,
        out Intersection hit)
    {
        hit = self
            .Where(x => x.T >= 0)
            .Order()
            .FirstOrDefault();
        return hit != null;
    }
}