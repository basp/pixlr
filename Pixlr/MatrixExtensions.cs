namespace Pixlr
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using Pixlr.Lina;

    public static class MatrixExtensions
    {
        public static Bitmap ToBitmap<U>(this Matrix<U> self, Func<U, Color> f)
            where U : struct, IEquatable<U>, IFormattable
        {
            var bmp = new Bitmap(self.ColumnCount, self.RowCount);
            using (var data = bmp.Lock())
            {
                Parallel.For(0, self.RowCount, y =>
                {
                    for (var x = 0; x < self.ColumnCount; x++)
                    {
                        var u = self[y, x];
                        var v = f(u);
                        data.At(x, y, v);
                    }
                });
            }

            return bmp;
        }
    }
}