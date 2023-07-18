namespace Pixlr.Lina
{
    using System;

    public interface IConvolution2D<U>
        where U : struct, IEquatable<U>, IFormattable
    {
        Matrix<U> Valid(Matrix<U> u);
        Matrix<U> Same(Matrix<U> u);
        Matrix<U> All(Matrix<U> u);
    }
}