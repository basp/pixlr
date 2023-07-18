namespace Pixlr.Lina
{
    using System;

    public class SparseCompressedMatrixStorage<T> : MatrixStorage<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        public static readonly T Zero = default(T);

        public readonly int[] RowPointers;

        public int[] ColumnIndices;

        public T[] Values;

        public int ValueCount => this.RowPointers[this.RowCount];

        internal SparseCompressedMatrixStorage(int rows, int columns)
            : base(rows, columns)
        {
            this.RowPointers = new int[rows + 1];
            this.ColumnIndices = new int[0];
            this.Values = new T[0];
        }

        public override bool IsDense => false;

        public override T At(int row, int column)
        {
            var index = this.FindItemIndex(row, column);
            return index >= 0 ? this.Values[index] : Zero;
        }

        public override void At(int row, int column, T value)
        {
            var index = this.FindItemIndex(row, column);
            if (index >= 0)
            {
                if (Zero.Equals(value))
                {
                    this.RemoveAtIndexUnchecked(index, row);
                }
                else
                {
                    this.Values[index] = value;
                }
            }
            else
            {
                if (Zero.Equals(value))
                {
                    return;
                }

                index = ~index;
                var valueCount = this.RowPointers[this.RowPointers.Length - 1];

                if ((valueCount == this.Values.Length) && (valueCount < ((long)this.RowCount * this.ColumnCount)))
                {
                    var size = Math.Min(this.Values.Length + this.GrowthSize(), (long)this.RowCount * this.ColumnCount);
                    if (size > int.MaxValue)
                    {
                        throw new NotSupportedException();
                    }

                    Array.Resize(ref this.Values, (int)size);
                    Array.Resize(ref this.ColumnIndices, (int)size);
                }

                Array.Copy(this.Values, index, this.Values, index + 1, valueCount - index);
                Array.Copy(this.ColumnIndices, index, this.ColumnIndices, index + 1, valueCount - index);

                this.Values[index] = value;
                this.ColumnIndices[index] = column;

                for (var i = row + 1; i < this.RowPointers.Length; i++)
                {
                    this.RowPointers[i] += 1;
                }
            }
        }

        public int FindItemIndex(int row, int column) =>
            Array.BinarySearch(
                this.ColumnIndices,
                this.RowPointers[row],
                this.RowPointers[row + 1] - this.RowPointers[row],
                column);

        private void RemoveAtIndexUnchecked(int index, int row)
        {
            var valueCount = this.RowPointers[this.RowPointers.Length - 1];

            Array.Copy(
                this.Values,
                index + 1,
                this.Values,
                index,
                valueCount - index - 1);

            Array.Copy(
                this.ColumnIndices,
                index + 1,
                this.ColumnIndices,
                index,
                valueCount - index - 1);

            for (var i = row + 1; i < this.RowPointers.Length; i++)
            {
                this.RowPointers[i] -= 1;
            }

            valueCount -= 1;

            if ((valueCount > 1024) && (valueCount < this.Values.Length / 2))
            {
                Array.Resize(ref this.Values, valueCount);
                Array.Resize(ref this.ColumnIndices, valueCount);
            }
        }

        private int GrowthSize()
        {
            int delta;
            if (this.Values.Length > 1024)
            {
                delta = Values.Length / 4;
            }
            else
            {
                if (Values.Length > 256)
                {
                    delta = 512;
                }
                else
                {
                    delta = Values.Length > 64 ? 128 : 32;
                }
            }

            return delta;
        }
    }
}