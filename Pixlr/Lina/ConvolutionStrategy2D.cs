namespace Pixlr.Lina
{
    using System;

    internal static class ConvolutionStrategy2D
    {
        public static ConvolutionStrategy2D<U, V> Create<U, V>(
            Accumulator<U, V> acc,
            Func<int, int, U> factory,
            Action<ConvolutionStrategy2D<U, V>> cfg)
            where U : struct
            where V : struct
        {
            var strat = new ConvolutionStrategy2D<U, V>(acc, factory);
            cfg(strat);
            return strat;
        }
    }

    internal class ConvolutionStrategy2D<U, V>
        where U : struct
        where V : struct
    {
        private readonly Accumulator<U, V> acc;
        private readonly Func<int, int, U> factory;

        public ConvolutionStrategy2D(
            Accumulator<U, V> acc,
            Func<int, int, U> factory)
        {
            this.acc = acc;
            this.factory = factory;
        }

        public Vector<int> StartInclusive { get; set; }

        public Vector<int> StopExclusive { get; set; }

        public Vector<int> TargetSize { get; set; }

        public U Accumulate(U s, U u, V v) => this.acc(s, u, v);

        public U GetMissingValue(int row, int col) => this.factory(row, col);
    }
}