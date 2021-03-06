namespace Pixlr.Lina
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MatrixExtensions
    {
        public static IEnumerable<V> FlatMap<U, V>(this Matrix<U> self, Func<U, V> f)
            where U : struct, IEquatable<U>, IFormattable
            => self.EnumerateColumns()
                .SelectMany(u => u.Enumerate())
                .Select(u => f(u));

        public static Vector<double> MeanOfColumns(this Matrix<int> self) =>
            self.EnumerateColumns().Select(x => x.Mean()).ToVector();

        public static Vector<double> MeanOfColumns(this Matrix<double> self) =>
            self.EnumerateColumns().Select(x => x.Mean()).ToVector();

        public static Vector<double> MeanOfRows(this Matrix<int> self) =>
            self.EnumerateRows().Select(x => x.Mean()).ToVector();

        public static Vector<double> MeanOfRows(this Matrix<double> self) =>
            self.EnumerateRows().Select(x => x.Mean()).ToVector();

        public static Vector<double> Centroid(this Matrix<int> self)
        {
            var col = self.EnumerateColumns()
                .Select(c => c.Centroid())
                .ToVector()
                .Centroid();

            var row = self.EnumerateRows()
                .Select(c => c.Centroid())
                .ToVector()
                .Centroid();

            return Vector.Build<double>().Dense(row, col);
        }

        public static Vector<double> Centroid(this Matrix<double> self)
        {
            var col = self.EnumerateColumns()
                .Select(c => c.Centroid())
                .ToVector()
                .Centroid();

            var row = self.EnumerateRows()
                .Select(c => c.Centroid())
                .ToVector()
                .Centroid();

            return Vector.Build<double>().Dense(row, col);
        }

        public static Matrix<T> Crop<T>(
            this Matrix<T> self,
            int row,
            int col,
            int height,
            int width)
            where T : struct, IEquatable<T>, IFormattable
        {
            throw new NotImplementedException();
        }
    }
}