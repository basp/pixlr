namespace Pixlr.Lina
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class VectorStorage<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        public readonly int Length;

        protected VectorStorage(int length)
        {
            this.Length = length;
        }

        public abstract bool IsDense { get; }

        public T this[int index]
        {
            get
            {
                this.ValidateRange(index);
                return this.At(index);
            }
            set
            {
                this.ValidateRange(index);
                this.At(index, value);
            }
        }

        public abstract T At(int index);

        public abstract void At(int index, T value);

        public virtual T[] ToArray() => this.Enumerate().ToArray();

        public virtual IEnumerable<T> Enumerate() =>
            Enumerable.Range(0, this.Length).Select(i => this.At(i));

        public virtual IEnumerable<Tuple<int, T>> EnumerateIndexed() =>
            Enumerable.Range(0, this.Length).Select(i => Tuple.Create(i, this.At(i)));

        private void ValidateRange(int index)
        {
            if (index < 0 || index >= this.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
    }
}