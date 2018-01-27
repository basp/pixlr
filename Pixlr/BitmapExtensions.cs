namespace Pixlr
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using Pixlr.Lina;

    public static class BitmapExtensions
    {
        public static LockedBitmapData Lock(
            this Bitmap self,
            ImageLockMode mode = ImageLockMode.ReadWrite,
            PixelFormat fmt = PixelFormat.Format24bppRgb) =>
                LockedBitmapData.Create(self);

        public static LockedBitmapData Lock(
            this Bitmap self,
            Rectangle rect,
            ImageLockMode mode = ImageLockMode.ReadWrite,
            PixelFormat fmt = PixelFormat.Format24bppRgb) =>
                LockedBitmapData.Create(self, rect);

        public static Matrix<double> ToMatrix(this Bitmap self) =>
            self.ToMatrix(c => c.Lum());

        public static Matrix<U> ToMatrix<U>(this Bitmap self, Func<Color, U> f)
            where U : struct, IEquatable<U>, IFormattable
        {
            using (var data = self.Lock(ImageLockMode.ReadOnly))
            {
                return data.ToMatrix(f);
            }
        }
    }
}
