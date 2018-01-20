namespace Pixlr.Lina
{
    using System;

    internal class ConvolutionStrategy2D
    {
        public ConvolutionStrategy2D()
        {
        }

        public Vector<int> StartInclusive { get; set; }

        public Vector<int> StopExclusive { get; set; }

        public Vector<int> TargetSize { get; set; }
    }
}