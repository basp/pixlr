namespace Pixlr.Lina
{
    using System;

    internal class ConvolutionStrategy1D
    {
        public ConvolutionStrategy1D()
        {
        }

        public int StartInclusive { get; set; }

        public int StopExclusive { get; set; }

        public int TargetLength { get; set; }
    }
}