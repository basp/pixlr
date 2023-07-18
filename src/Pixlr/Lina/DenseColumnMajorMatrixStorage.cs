namespace Pixlr.Lina
{
    using System;

    public class DenseColumnMajorMatrixStorage<T> : MatrixStorage<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        public readonly T[] Data;

        internal DenseColumnMajorMatrixStorage(int rows, int columns)
            : base(rows, columns)
        {
            Data = new T[rows * columns];
        }

        internal DenseColumnMajorMatrixStorage(int rows, int columns, T[] data)
            : base(rows, columns)
        {
            Data = data;
        }

        public override bool IsDense => true;

        public override T At(int row, int column) =>
            this.Data[(column * this.RowCount) + row];

        public override void At(int row, int column, T value) =>
            this.Data[(column * this.RowCount) + row] = value;

        private void RowColumnAtIndex(int index, out int row, out int col)
        {
            col = Math.DivRem(index, this.RowCount, out row);
        }
    }
}