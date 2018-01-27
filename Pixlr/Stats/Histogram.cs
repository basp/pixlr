namespace Pixlr.Stats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Histogram
    {
        private readonly Bucket[] buckets;

        private Histogram(IEnumerable<Bucket> buckets)
        {
            this.buckets = buckets.ToArray();
        }

        public static Histogram Create(IEnumerable<double> data, int nbuckets)
        {
            data = data.ToArray();

            var min = data.Min();
            var max = data.Max();
            var step = (max - min) / nbuckets;
            var buckets = Enumerable
                .Range(0, nbuckets)
                .Select(i => new Bucket(i * step, i * step + step))
                .OrderBy(b => b.LowerBound)
                .ToArray();

            // Steps usually don't add up exactly to max, just force it
            buckets[buckets.Length - 1].UpperBound = max;

            var hist = new Histogram(buckets);
            Array.ForEach(data.ToArray(), v => hist.GetBucketOf(v).Count++);
            return hist;
        }

        public Bucket this[int i]
        {
            get => this.buckets[i];
        }

        public int BucketCount => this.buckets.Length;

        private static bool ValueInRange(double v, Bucket bucket) =>
            v >= bucket.LowerBound && v <= bucket.UpperBound;

        public Bucket GetBucketOf(double value)
        {
            for (var i = 0; i < this.BucketCount; i++)
            {
                if (ValueInRange(value, this[i]))
                {
                    return this[i];
                }
            }

            return null;
        }

        public int GetBucketIndexOf(double value)
        {
            for (var i = 0; i < this.BucketCount; i++)
            {
                if (ValueInRange(value, this[i]))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}