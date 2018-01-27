namespace Pixlr.Lina
{
    using System;
    using System.Threading.Tasks;

    internal class Convolution1D<U, V> : IConvolution1D<U>
        where U : struct, IEquatable<U>, IFormattable
        where V : struct, IEquatable<V>, IFormattable
    {
        private readonly Vector<V> v;   // weights (should have odd number of elements)
        private readonly int vc;        // v center index (e.g. 2 in a `v` of length 5)
        private readonly Accumulator<U, V> acc;
        private readonly Func<int, U> factory;

        public Convolution1D(
            Vector<V> v,
            Accumulator<U, V> acc,
            Func<int, U> factory)
        {
            this.v = v;
            this.vc = v.Length / 2;
            this.acc = acc;
            this.factory = factory;
        }

        public Vector<U> Valid(Vector<U> u)
        {
            var strat = new ConvolutionStrategy1D
            {
                FromInclusive = this.vc,
                ToExclusive = u.Length - this.vc,
                TargetLength = u.Length - 2 * this.vc,
            };

            return this.Convolve(u, strat);
        }

        public Vector<U> Same(Vector<U> u)
        {
            var strat = new ConvolutionStrategy1D
            {
                FromInclusive = 0,
                ToExclusive = u.Length,
                TargetLength = u.Length,
            };

            return this.Convolve(u, strat);
        }

        public Vector<U> All(Vector<U> u)
        {
            var strat = new ConvolutionStrategy1D
            {
                FromInclusive = -this.vc,
                ToExclusive = u.Length + this.vc,
                TargetLength = u.Length + 2 * this.vc,
            };

            return this.Convolve(u, strat);
        }

        internal U Accumulate(int i, Vector<U> u)
        {
            var s = default(U);
            for (var k = -this.vc; k <= this.vc; k++)
            {
                var ii = i + k;                     // calculate inner index
                var vv = this.v[k + this.vc];       // get weight (v) value from kernel
                var uv = ii < 0 || ii >= u.Length   // is inner index out of range?
                    ? this.factory(i)               // then fake (u) value
                    : u[ii];                        // otherwise source (u) value

                s = this.acc(s, uv, vv);
            }

            return s;
        }

        internal Vector<U> Convolve(Vector<U> u, ConvolutionStrategy1D strat)
        {
            var w = Vector.Build<U>().Dense(strat.TargetLength, _ => default(U));
            Parallel.For(strat.FromInclusive, strat.ToExclusive, i =>
            {
                var s = this.Accumulate(i, u);
                w[i - strat.FromInclusive] = s;
            });

            return w;
        }
    }
}