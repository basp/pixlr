namespace Pixlr.Lina
{
    using System;

    // public delegate U Accumulator<U, V>(U s, U u, V v);

    internal class Convolution2D<U, V> : IConvolution2D<U>
        where U : struct, IEquatable<U>
        where V : struct, IEquatable<V>
    {
        private readonly Vector<V> v;   // weights (should have odd number of elements)
        private readonly int vc;        // v center index (e.g. 2 in a `v` of length 5)
        private readonly Accumulator<U, V> acc;
        private readonly Func<int, int, U> factory;

        public Convolution2D(
            Vector<V> v,
            Accumulator<U, V> acc,
            Func<int, int, U> factory)
        {
            this.v = v;
            this.vc = v.Length / 2;
            this.acc = acc;
            this.factory = factory;
        }

        public Matrix<U> Valid(Matrix<U> u)
        {
            var strat = ConvolutionStrategy2D.Create(this.acc, factory, cfg =>
            {
                // cfg.StartInclusive = this.vc;
                // cfg.StopExclusive = u.Length - this.vc;
                // cfg.TargetLength = u.Length - 2 * this.vc;
            });

            return this.Convolve(u, strat);
        }

        public Matrix<U> Same(Matrix<U> u)
        {
            var strat = ConvolutionStrategy2D.Create(this.acc, this.factory, cfg =>
            {
                // cfg.StartInclusive = 0;
                // cfg.StopExclusive = u.Length;
                // cfg.TargetLength = u.Length;
            });

            return this.Convolve(u, strat);
        }

        public Matrix<U> All(Matrix<U> u)
        {
            var strat = ConvolutionStrategy2D.Create(this.acc, this.factory, cfg =>
            {
                // cfg.StartInclusive = -this.vc;
                // cfg.StopExclusive = u.Length + this.vc;
                // cfg.TargetLength = u.Length + 2 * this.vc;
            });

            return this.Convolve(u, strat);
        }

        internal Matrix<U> Convolve(Matrix<U> u, ConvolutionStrategy2D<U, V> strat)
        {
            throw new NotImplementedException();

            // var w = Vector.Build<U>().Dense(strat.TargetLength, _ => default(U));
            // for (var i = strat.StartInclusive; i < strat.StopExclusive; i++)
            // {
            //     var s = default(U);
            //     for (var k = -this.vc; k <= this.vc; k++)
            //     {
            //         var ii = i + k;                     // calculate inner index
            //         var vv = this.v[k + this.vc];       // get weight (v) value from kernel
            //         var uv = ii < 0 || ii >= u.Length   // is inner index out of range?
            //             ? strat.GetMissingValue(i)      // then fake (u) value
            //             : u[ii];                        // otherwise source (u) value

            //         s = strat.Accumulate(s, uv, vv);
            //     }

            //     w[i - strat.StartInclusive] = s;
            // }

            // return w;
        }
    }
}