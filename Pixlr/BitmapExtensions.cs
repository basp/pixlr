namespace Pixlr
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

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
    }
}
