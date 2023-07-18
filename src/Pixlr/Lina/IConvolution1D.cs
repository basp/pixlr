namespace Pixlr.Lina
{
    using System;

    public interface IConvolution1D<U>
        where U : struct, IEquatable<U>, IFormattable
    {
        Vector<U> Valid(Vector<U> u);
        Vector<U> Same(Vector<U> u);
        Vector<U> All(Vector<U> u);
    }
}