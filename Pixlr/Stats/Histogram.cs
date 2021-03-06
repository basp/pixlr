namespace Pixlr.Stats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Optional;
    using Optional.Unsafe;

    public class Histogram
    {
        public enum InitializationMode
        {
            None = 0,           // will default to parallel
            Sequential = 1,     // better for small data sets
            Parallel = 2,       // better for large data sets
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
            data = data.ToArray();

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

        private static string NoBucketForMsg(double value) =>
            $"No bucket found for {value}";

        private void InitializeParallel(IEnumerable<double> data) =>
            Parallel.ForEach(
                data,
                v => this
                    .GetBucketOf(v)
                    .ValueOrFailure(NoBucketForMsg(v)).Count++);

        private void InitializeSequential(IEnumerable<double> data) =>
            Array.ForEach(
                data.ToArray(),
                v => this
                    .GetBucketOf(v)
                    .ValueOrFailure(NoBucketForMsg(v)).Count++);

        public Bucket this[int i]
        {
            get => this.buckets[i];
        }

        public int BucketCount => this.buckets.Length;

        private static bool ValueInBucket(double v, Bucket bucket) =>
            v >= bucket.LowerBound && v <= bucket.UpperBound;

        public Option<Bucket> GetBucketOf(double value)
        {
            for (var i = 0; i < this.BucketCount; i++)
            {
                if (ValueInBucket(value, this[i]))
                {
                    return Option.Some(this[i]);
                }
            }

            return Option.None<Bucket>();
        }

        public Option<int> GetBucketIndexOf(double value)
        {
            for (var i = 0; i < this.BucketCount; i++)
            {
                if (ValueInBucket(value, this[i]))
                {
                    return Option.Some(i);
                }
            }

            return Option.None<int>();
        }
    }
}