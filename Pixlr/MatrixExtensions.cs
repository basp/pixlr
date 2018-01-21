namespace Pixlr
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Pixlr.Lina;

    public static class MatrixExtensions
    {
        public static Bitmap ToBitmap<U>(this Matrix<U> self, Func<U, Color> f)
            where U : struct, IEquatable<U>
        {
            var bmp = new Bitmap(self.ColumnCount, self.RowCount);
            using (var data = bmp.Lock())
            {
                for (var y = 0; y < self.RowCount; y++)
                {
                    for (var x = 0; x < self.ColumnCount; x++)
                    {
                        var u = self[y, x];
                        var v = f(u);
                        data.At(x, y, v);
                    }
                }
            }

            return bmp;
        }
    }
}