namespace Pixlr
{
    using Pixlr.Lina;
    using Pixlr.Stats;

    public static class VectorExtensions
    {
        public static Histogram ToHistogram(this Vector<double> self, int nbuckets) =>
            Histogram.Create(self.Enumerate(), nbuckets);
    }
}