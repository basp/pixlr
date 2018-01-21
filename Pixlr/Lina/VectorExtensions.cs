namespace Pixlr.Lina
{
    using System;
    using System.Linq;

    public static class VectorExtensions
    {
        public static double Sum(this Vector<int> self)
            => self.Enumerate().Sum();

        public static double Sum(this Vector<double> self)
            => self.Enumerate().Sum();

        public static T Aggregate<T>(this Vector<T> self, Func<T, T, T> acc)
            where T : struct, IEquatable<T>
            => self.Aggregate(default(T), acc);

        public static T Aggregate<T>(this Vector<T> self, T seed, Func<T, T, T> acc)
            where T : struct, IEquatable<T>
            => self.Enumerate().Aggregate(seed, acc);

        // public static double Centroid<T>(
        //     this Vector<T> self, 
        //     Func<T, int, T> sel,
        //     Func<T, T, T> agg)
        //     where T: struct, IEquatable<T>
        // {
        //     var t = self.Enumerate().Select(sel).Aggregate(agg);
        // }

        public static double Centroid(this Vector<int> self)
        {
            var t = self.Enumerate().Select((x, i) => (i + 1) * x).Sum();
            var s = self.Sum();
            return s > 0 ? (t / s) - 1 : 0;
        }

        public static double Centroid(this Vector<double> self)
        {
            var t = self.Enumerate().Select((x, i) => (i + 1) * x).Sum();
            var s = self.Sum();
            return s > 0 ? (t / s) - 1 : 0;
        }
    }
}