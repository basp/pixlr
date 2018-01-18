namespace Pixlr.Lina
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Matrix
    {
        public static Matrix<T> Create<T>(int rows, int cols, params T[] storage)
            where T : struct => new Matrix<T>(rows, cols, storage);

        public static Matrix<T> Create<T>(int rows, int cols, Func<int, int, T> factory)
            where T : struct => new Matrix<T>(
                rows,
                cols,
                CreateValues(rows, cols, factory).ToArray());

        private static IEnumerable<T> CreateValues<T>(
            int rows,
            int cols,
            Func<int, int, T> factory)
        {
            for (var col = 0; col < cols; col++)
            {
                for (var row = 0; row < rows; row++)
                {
                    yield return factory(row, col);
                }
            }
        }
    }

    public class Matrix<T> where T : struct
    {
        private readonly T[] storage;
        private int rows;
        private int cols;

        public Matrix(int rows, int cols, params T[] storage)
        {
            this.rows = rows;
            this.cols = cols;
            this.storage = storage;
        }

        public T this[int row, int col]
        {
            get => this.storage[this.GetIndex(row, col)];
            set => this.storage[this.GetIndex(row, col)] = value;
        }

        public int RowCount => this.rows;

        public int ColumnCount => this.cols;

        public T At(int row, int col) => this[row, col];

        private int GetIndex(int row, int col) => col * this.rows + row;
    }
}