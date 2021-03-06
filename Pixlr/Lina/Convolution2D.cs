namespace Pixlr.Lina
{
    using System;
    using System.Threading.Tasks;

    internal class Convolution2D<U, V> : IConvolution2D<U>
        where U : struct, IEquatable<U>, IFormattable
        where V : struct, IEquatable<V>, IFormattable
    {
        private readonly Matrix<V> v;   // kernel   
        private readonly Vector<int> vc;
        private readonly Accumulator<U, V> acc;
        private readonly Func<int, int, U> factory;

        public Convolution2D(
            Matrix<V> v,
            Accumulator<U, V> acc,
            Func<int, int, U> factory)
        {
            this.v = v;
            this.vc = Vector.Build<int>().Dense(v.RowCount / 2, v.ColumnCount / 2);
            this.acc = acc;
            this.factory = factory;
        }

        public Matrix<U> Valid(Matrix<U> u)
        {
            var strat = new ConvolutionStrategy2D
            {
                FromInclusive = this.vc,

                ToExclusive = Vector.Build<int>().Dense(
                    u.RowCount - this.vc[0],
                    u.ColumnCount - this.vc[1]),

                TargetSize = Vector.Build<int>().Dense(
                    u.RowCount - 2 * this.vc[0],
                    u.ColumnCount - 2 * this.vc[1]),
            };

            return this.Convolve(u, strat);
        }

        public Matrix<U> Same(Matrix<U> u)
        {
            var strat = new ConvolutionStrategy2D
            {
                FromInclusive = Vector.Build<int>().Dense(0, 0),

                ToExclusive = Vector.Build<int>().Dense(
                    u.RowCount,
                    u.ColumnCount),

                TargetSize = Vector.Build<int>().Dense(
                    u.RowCount,
                    u.ColumnCount),
            };

            return this.Convolve(u, strat);
        }

        public Matrix<U> All(Matrix<U> u)
        {
            var strat = new ConvolutionStrategy2D
            {
                FromInclusive = Vector.Build<int>().Dense(
                    -this.vc[0],
                    -this.vc[1]),

                ToExclusive = Vector.Build<int>().Dense(
                    u.RowCount + this.vc[0],
                    u.ColumnCount + this.vc[1]),

                TargetSize = Vector.Build<int>().Dense(
                    u.RowCount + 2 * this.vc[0],
                    u.ColumnCount + 2 * this.vc[1]),
            };

            return this.Convolve(u, strat);
        }

        internal U Accumulate(int row, int col, Matrix<U> u)
        {
            var s = default(U);
            for (var i = -this.vc[0]; i <= this.vc[0]; i++)
            {
                for (var j = -this.vc[1]; j <= this.vc[1]; j++)
                {
                    var ii = row + i;
                    var jj = col + j;

                    var vv = this.v[i + this.vc[0], j + this.vc[1]];
                    var uv = ii < 0 || ii >= u.RowCount || jj < 0 || jj >= u.ColumnCount
                        ? this.factory(ii, jj)
                        : u[ii, jj];

                    s = this.acc(s, uv, vv);
                }
            }

            return s;
        }

        internal Matrix<U> Convolve(Matrix<U> u, ConvolutionStrategy2D strat)
        {
            var w = Matrix.Create(
                strat.TargetSize[0],
                strat.TargetSize[1],
                (r, c) => default(U));

            Parallel.For(strat.FromInclusive[0], strat.ToExclusive[0], r =>
            {
                for (var c = strat.FromInclusive[1]; c < strat.ToExclusive[1]; c++)
                {
                    var s = this.Accumulate(r, c, u);
                    w[r - strat.FromInclusive[0], c - strat.FromInclusive[1]] = s;
                }
            });

            return w;
        }
    }
}