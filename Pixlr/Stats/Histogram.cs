namespace Pixlr.Stats
{
    using System;
    using System.Collections.Generic;

    public class Histogram
    {
        private readonly IEnumerable<Bucket> buckets;

        private Histogram(IEnumerable<Bucket> buckets)
        {
            this.buckets = buckets;
        }

        public static Histogram Create(IEnumerable<double> data, int nbuckets)
        {
            throw new NotImplementedException();
        }
    }
}