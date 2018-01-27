namespace Pixlr.Lina
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class Matrix
    {
        public static Matrix<T> Create<T>(int rows, int cols, params T[] storage)
            where T : struct, IEquatable<T>, IFormattable
            => new Matrix<T>(rows, cols, storage);

        public static Matrix<T> Create<T>(int rows, int cols)
            where T : struct, IEquatable<T>, IFormattable
            => Create<T>(rows, cols, (r, c) => default(T));

        public static Matrix<T> Create<T>(int rows, int cols, Func<int, int, T> factory)
            where T : struct, IEquatable<T>, IFormattable
            => new Matrix<T>(rows, cols, CreateValues(rows, cols, factory).ToArray());

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

    public class Matrix<T>
        where T : struct, IEquatable<T>, IFormattable
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

        public void At(int row, int col, T value) => this[row, col] = value;

        public IEnumerable<Vector<T>> EnumerateRows()
        {
            for (var r = 0; r < this.RowCount; r++)
            {
                var storage = Enumerable
                    .Range(0, this.ColumnCount)
                    .Select(c => this[r, c])
                    .ToArray();

                yield return Vector.Build<T>().Dense(storage);
            }
        }

        public IEnumerable<Vector<T>> EnumerateColumns()
        {
            for (var c = 0; c < this.ColumnCount; c++)
            {
                var storage = Enumerable
                    .Range(0, this.RowCount)
                    .Select(r => this[r, c])
                    .ToArray();

                yield return Vector.Build<T>().Dense(storage);
            }
        }

        public void MapInPlace(Func<T, T> f)
        {
            Parallel.For(0, this.rows, r =>
            {
                for (var c = 0; c < this.cols; c++)
                {
                    this.At(r, c, f(this.At(r, c)));
                }
            });
        }

        public IConvolution2D<U> Convolution<U>(
            Accumulator<U, T> acc,
            Func<int, int, U> factory)
            where U : struct, IEquatable<U>, IFormattable
        {
            this.ValidateKernel();
            return new Convolution2D<U, T>(this, acc, factory);
        }

        private int GetIndex(int row, int col) => col * this.rows + row;

        private void ValidateKernel()
        {
            if (this.RowCount % 2 == 0)
            {
                var msg = $"You can only create a convolution kernel instance from a matrix with an odd size and this matrix has {this.RowCount} rows.";
                throw new ArgumentException(msg, nameof(this.RowCount));
            }

            if (this.ColumnCount % 2 == 0)
            {
                var msg = $"You can only create a convolution kernel instance from a matrix with an odd size and this matrix has {this.ColumnCount} columns.";
                throw new ArgumentException(msg, nameof(this.ColumnCount));
            }
        }
    }
}