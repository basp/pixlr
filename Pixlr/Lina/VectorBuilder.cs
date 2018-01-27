namespace Pixlr.Lina
{
    using System;

    public class VectorBuilder<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        public Vector<T> Dense(VectorStorage<T> storage) =>
            new Vector<T>(storage);

        public Vector<T> Dense(int length) =>
            new Vector<T>(new DenseVectorStorage<T>(length));

        public Vector<T> Dense(int length, Func<int, T> factory) =>
            new Vector<T>(new DenseVectorStorage<T>(length, factory));

        public Vector<T> Dense(params T[] data) =>
            new Vector<T>(new DenseVectorStorage<T>(data.Length, data));
    }
}