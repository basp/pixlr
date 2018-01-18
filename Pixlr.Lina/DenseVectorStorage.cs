namespace Pixlr.Lina
{
    using System;

    public class DenseVectorStorage<T> : VectorStorage<T>
        where T : struct, IEquatable<T>
    {
        public readonly T[] Data;

        internal DenseVectorStorage(int length)
            : base(length)
        {
            this.Data = new T[length];
        }

        internal DenseVectorStorage(int length, Func<int, T> factory)
            : this(length)
        {
            for (var i = 0; i < length; i++)
            {
                this.At(i, factory(i));
            }
        }

        internal DenseVectorStorage(int length, T[] data)
            : base(length)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length != length)
            {
                throw new ArgumentOutOfRangeException(nameof(data));
            }

            this.Data = data;
        }

        public override bool IsDense => true;

        public override T At(int index) => this.Data[index];

        public override void At(int index, T value)
        {
            this.Data[index] = value;
        }
    }
}