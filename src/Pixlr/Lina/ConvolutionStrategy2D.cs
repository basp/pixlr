namespace Pixlr.Lina
{
    using System;

    internal class ConvolutionStrategy2D
    {
        public Vector<int> FromInclusive { get; set; }

        public Vector<int> ToExclusive { get; set; }

        public Vector<int> TargetSize { get; set; }
    }
}