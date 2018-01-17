namespace Pixlr
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class LockedBitmapData : IDisposable
    {
        private bool disposed = false;

        private readonly Bitmap src;

        private readonly BitmapData data;

        private LockedBitmapData(Bitmap src, BitmapData data)
        {
            this.src = src;
            this.data = data;
        }

        public Color this[int row, int column]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public static LockedBitmapData Create(Bitmap src) =>
            new LockedBitmapData(src, LockBits(src));

        private static BitmapData LockBits(
            Bitmap src,
            ImageLockMode mode = ImageLockMode.ReadWrite,
            PixelFormat fmt = PixelFormat.Format24bppRgb) =>
                src.LockBits(
                    new Rectangle(0, 0, src.Width, src.Height),
                    mode,
                    PixelFormat.Format24bppRgb);

        private static Rectangle RectangleFromSource(Bitmap src) =>
            new Rectangle(0, 0, src.Width, src.Height);

        public unsafe void MapInPlace(Func<Color, Color> f)
        {
            const int BytesPerPixel = 3;
            var scan0 = (byte*)data.Scan0;
            var stride = data.Stride;
            for (var y = 0; y < data.Height; y++)
            {
                var row = scan0 + (y * stride);
                for (var x = 0; x < data.Width; x++)
                {
                    // Note that order is BGR (blame Microsoft)
                    var bi = x * BytesPerPixel;
                    var gi = bi + 1;
                    var ri = bi + 2;

                    var r = row[ri];
                    var g = row[gi];
                    var b = row[bi];

                    var u = Color.FromArgb(r, g, b);
                    var v = f(u);
                    row[ri] = v.R;
                    row[gi] = v.G;
                    row[bi] = v.B;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.src.UnlockBits(data);
                }

                this.disposed = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
    }

    public static class BitmapExtensions
    {
        public static Rectangle Rect(this Bitmap self) =>
            new Rectangle(0, 0, self.Width, self.Height);

        public static LockedBitmapData Lock(
            this Bitmap self,
            ImageLockMode mode = ImageLockMode.ReadWrite,
            PixelFormat fmt = PixelFormat.Format24bppRgb) =>
                LockedBitmapData.Create(self);
    }
}
