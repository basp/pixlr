namespace Pixlr.Lina
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class MatrixStorage<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        public readonly int RowCount;

        public readonly int ColumnCount;

        public MatrixStorage(int rowCount, int columnCount)
        {
            this.RowCount = rowCount;
            this.ColumnCount = columnCount;
        }

        public T this[int row, int column]
        {
            get
            {
                this.ValidateRange(row, column);
                return this.At(row, column);
            }
            set
            {
                this.ValidateRange(row, column);
                this.At(row, column, value);
            }
        }

        public abstract bool IsDense { get; }

        public abstract T At(int row, int column);

        public abstract void At(int row, int column, T value);

        private void ValidateRange(int row, int col)
        {
            if (row < 0 || row >= this.RowCount)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (col < 0 || col >= this.ColumnCount)
            {
                throw new ArgumentOutOfRangeException(nameof(col));
            }
        }
    }
}