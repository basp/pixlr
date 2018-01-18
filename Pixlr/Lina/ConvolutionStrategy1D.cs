namespace Pixlr.Lina
{
    using System;

    internal static class ConvolutionStrategy1D
    {
        public static ConvolutionStrategy1D<U, V> Create<U, V>(
            Accumulator<U, V> acc,
            Func<int, U> factory,
            Action<ConvolutionStrategy1D<U, V>> cfg)
            where U : struct
            where V : struct
        {
            var strat = new ConvolutionStrategy1D<U, V>(acc, factory);
            cfg(strat);
            return strat;
        }
    }

    internal class ConvolutionStrategy1D<U, V>
        where U : struct
        where V : struct
    {
        private readonly Accumulator<U, V> acc;
        private readonly Func<int, U> factory;

        public ConvolutionStrategy1D(
            Accumulator<U, V> acc,
            Func<int, U> factory)
        {
            this.acc = acc;
            this.factory = factory;
        }

        public int StartInclusive { get; set; }

        public int StopExclusive { get; set; }

        public int TargetLength { get; set; }

        public U Accumulate(U s, U u, V v) => this.acc(s, u, v);

        public U GetMissingValue(int i) => this.factory(i);
    }
}