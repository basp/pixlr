namespace Pixlr
{
    using System.Collections.Generic;
    using Pixlr.Stats;

    public static class EnumerableExtensions
    {
        public static Histogram ToHistogram(
            this IEnumerable<double> self,
            int nbuckets) => Histogram.Create(self, nbuckets);
    }
}