namespace Pixlr.Lina
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Vector
    {
        public static VectorBuilder<T> Build<T>()
            where T : struct, IEquatable<T> => new VectorBuilder<T>();

        private static IEnumerable<T> CreateValues<T>(
            int count,
            Func<int, T> factory) where T : struct =>
                Enumerable.Range(0, count).Select(factory);
    }

    public class Vector<T> where T : struct, IEquatable<T>
    {
        private readonly VectorStorage<T> storage;

        public Vector(params T[] storage)
            : this(new DenseVectorStorage<T>(storage.Length, storage))
        {
        }

        internal Vector(VectorStorage<T> storage)
        {
            this.storage = storage;
        }

        public T this[int i]
        {
            get => this.storage[i];
            set => this.storage[i] = value;
        }

        public int Length => this.storage.Length;

        public void At(int i, T value) => this.storage.At(i, value);

        public T At(int i) => this.storage.At(i);

        public IEnumerable<T> Enumerate() => Enumerable
            .Range(0, this.Length)
            .Select(i => this.storage[i]);

        public IConvolution1D<U> Convolution<U>(
            Accumulator<U, T> acc,
            Func<int, U> factory)
            where U : struct, IEquatable<U>
        {
            this.ValidateKernel();
            return new Convolution1D<U, T>(this, acc, factory);
        }

        private void ValidateKernel()
        {
            if (this.Length % 2 == 0)
            {
                var msg = $"You can only create a convolution kernel instance from a vector with an odd size and this vector has {this.Length} elements.";
                throw new ArgumentException(msg, nameof(this.Length));
            }
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append("(");
            for(var i = 0; i < this.Length - 1; i++)
            {
                s.Append(this.At(i));
                s.Append(" ");
            }

            s.Append(this.At(this.Length - 1));
            s.Append(")");
            return s.ToString();
        }
    }
}
