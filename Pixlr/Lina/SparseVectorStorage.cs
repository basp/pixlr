namespace Pixlr.Lina
{
    using System;

    public class SparseVectorStorage<T> : VectorStorage<T>
        where T : struct, IEquatable<T>
    {
        private static readonly T Zero = default(T);

        public int[] Indices;

        public T[] Values;

        public int ValueCount;

        internal SparseVectorStorage(int length) : base(length)
        {
            this.Indices = new int[0];
            Values = new T[0];
            ValueCount = 0;
        }

        public override bool IsDense => false;

        public override T At(int index)
        {
            var itemIndex = Array.BinarySearch(this.Indices, 0, ValueCount, index);
            return itemIndex >= 0 ? this.Values[itemIndex] : default(T);
        }

        public override void At(int index, T value)
        {
            var itemIndex = Array.BinarySearch(this.Indices, 0, this.ValueCount, index);
            if (itemIndex >= 0)
            {
                if (Zero.Equals(value))
                {
                    this.RemoveAtIndexUnchecked(itemIndex);
                }
                else
                {
                    this.Values[itemIndex] = value;
                }
            }
            else
            {
                if (!Zero.Equals(value))
                {
                    this.InsertAtIndexUnchecked(itemIndex, index, value);
                }
            }
        }

        internal void InsertAtIndexUnchecked(int itemIndex, int index, T value)
        {
            if (this.ValueCount == this.Values.Length && this.ValueCount < this.Length)
            {
                var size = Math.Min(this.Values.Length + this.GrowthSize(), this.Length);
                Array.Resize(ref this.Values, size);
                Array.Resize(ref this.Indices, size);
            }

            Array.Copy(this.Values, itemIndex, this.Values, itemIndex + 1, this.ValueCount - itemIndex);
            Array.Copy(this.Indices, itemIndex, this.Indices, itemIndex + 1, this.ValueCount - itemIndex);

            this.Values[itemIndex] = value;
            this.Indices[itemIndex] = index;

            this.ValueCount += 1;
        }

        internal void RemoveAtIndexUnchecked(int itemIndex)
        {
            Array.Copy(this.Values, itemIndex + 1, this.Values, itemIndex, this.ValueCount - itemIndex - 1);
            Array.Copy(this.Indices, itemIndex + 1, this.Indices, itemIndex, this.ValueCount - itemIndex - 1);

            this.ValueCount -= 1;

            if (this.ValueCount > 1024 && (this.ValueCount < this.Indices.Length / 2))
            {
                Array.Resize(ref this.Values, this.ValueCount);
                Array.Resize(ref this.Indices, this.ValueCount);
            }
        }

        private int GrowthSize()
        {
            int delta;
            if (this.Values.Length > 1024)
            {
                delta = this.Values.Length / 4;
            }
            else if (this.Values.Length > 256)
            {
                delta = 512;
            }
            else
            {
                delta = this.Values.Length > 64 ? 128 : 32;
            }

            return delta;
        }
    }
}