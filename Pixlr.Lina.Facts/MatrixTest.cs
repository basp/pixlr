namespace Pixlr.Lina.Facts
{
    using System;
    using Xunit;

    public class MatrixTest
    {
        [Fact]
        public void StoresElementsInColumnMajorOrder()
        {
            var s = new[]
            {
                0.0, 1.0, 2.0, // col 0
                0.1, 1.1, 2.1, // col 1
            };

            var m = Matrix.Create(3, 2, s);

            Assert.Equal(2.0, m.At(2, 0));
            Assert.Equal(0.1, m[0, 1]);
        }

        [Fact]
        public void ReportsRowAndColumnCount()
        {
            var s = new int[] { 1, 2, 3 };
            var m = Matrix.Create(3, 1, s);

            Assert.Equal(3, m.RowCount);
            Assert.Equal(1, m.ColumnCount);
        }

        [Fact]
        public void MutateElementsInPlace()
        {
            var m = Matrix.Create(3, 3, (row, col) => 0);
            m[1, 1] = 1;

            Assert.Equal(0, m[0, 0]);
            Assert.Equal(1, m.At(1, 1));
        }
    }
}
