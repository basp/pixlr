namespace Pixlr.Stats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Histogram
    {
        public enum InitializationMode
        {
            None = 0,
            Sequential = 1,
            Parallel = 2,
        }

        private readonly Bucket[] buckets;

        private Histogram(IEnumerable<Bucket> buckets)
        {
            this.buckets = buckets.ToArray();
        }

        public static Histogram Create(double min, double max, int nbuckets)
        {
            var step = (max - min) / nbuckets;
            var buckets = Enumerable
                .Range(0, nbuckets)
                .Select(i => new Bucket(i * step, i * step + step))
                .OrderBy(b => b.LowerBound)
                .ToArray();

            // Steps usually don't add up exactly to max, just force it
            buckets[buckets.Length - 1].UpperBound = max;

            var hist = new Histogram(buckets);
            return hist;
        }

        public static Histogram Create(
            IEnumerable<double> data,
            int nbuckets,
            InitializationMode mode = InitializationMode.Parallel)
        {
            var min = data.Min();
            var max = data.Max();
            var hist = Create(min, max, nbuckets);
            switch (mode)
            {
                case InitializationMode.Sequential:
                    hist.InitializeSequential(data);
                    break;
                case InitializationMode.Parallel:
                    hist.InitializeParallel(data);
                    break;
                default:
                    hist.InitializeSequential(data);
                    break;
            }

            return hist;
        }

        public void InitializeParallel(IEnumerable<double> data) =>
            Parallel.ForEach(data, v => this.GetBucketOf(v).Count++);

        public void InitializeSequential(IEnumerable<double> data) =>
            Array.ForEach(data.ToArray(), v => this.GetBucketOf(v).Count++);

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