namespace Pixlr.Stats
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class HistogramExtensions
    {
        public static string Plot(this Histogram self)
        {
            var buckets = self.Enumerate().ToList();
            var unit = buckets.Max(x => x.Count) / 32.0;
            var sb = new StringBuilder();
            foreach(var b in buckets)
            {
                var size = (int)(b.Count / unit);
                var bar = "#".Repeat(size);
                sb.AppendFormat(
                    "{0:0.00} - {1:0.00} | {2}", 
                    b.LowerBound,
                    b.UpperBound,
                    bar);
                    
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static IEnumerable<Bucket> Enumerate(this Histogram self)
        {
            for (var i = 0; i < self.BucketCount; i++)
            {
                yield return self[i];
            }
        }

        public static Bucket Otsu(this Histogram self)
        {
            var buckets = self.Enumerate().ToList();
            var nbuckets = buckets.Count;
            var total = buckets.Sum(x => x.Count);
            var sum = buckets.Select((x, i) => i * x.Count).Sum();
            var sumB = 0.0;
            var wB = 0.0;
            var wF = 0.0;
            var max = 0.0;
            var threshold = 0;

            for (var i = 0; i < nbuckets; i++)
            {
                wB += self[i].Count;
                if (wB == 0)
                {
                    continue;
                }

                wF = total - wB;
                if (wF == 0)
                {
                    continue;
                }

                wF += i * self[i].Count;

                var mB = sumB / wB;
                var mF = (sum - sumB) / wF;

                var between = wB * wF * (mB - mF) * (mB - mF);

                if (between > max)
                {
                    max = between;
                    threshold = i;
                }
            }

            return buckets[threshold];
        }
    }
}