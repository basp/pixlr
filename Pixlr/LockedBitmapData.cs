namespace Pixlr
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using Lina;

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

        public unsafe Color this[int x, int y]
        {
            get => this.At(x, y);
            set => this.At(x, y, value);
        }

        public unsafe Color At(int x, int y)
        {
            var scan0 = (byte*)this.data.Scan0;
            var stride = this.data.Stride;
            var row = scan0 + (y * stride);
            var bi = x * BytesPerPixel;
            var gi = bi + 1;
            var ri = bi + 2;
            return Color.FromArgb(row[ri], row[gi], row[bi]);
        }

        public unsafe void At(int x, int y, Color color)
        {
            var scan0 = (byte*)this.data.Scan0;
            var stride = this.data.Stride;
            var row = scan0 + (y * stride);
            var bi = x * BytesPerPixel;
            var gi = bi + 1;
            var ri = bi + 2;
            row[ri] = color.R;
            row[gi] = color.G;
            row[bi] = color.B;
        }

        public static LockedBitmapData Create(Bitmap src) =>
            new LockedBitmapData(src, LockBits(src));

        public static LockedBitmapData Create(Bitmap src, Rectangle rect) =>
            new LockedBitmapData(src, LockBits(src, rect));

        private static BitmapData LockBits(
            Bitmap src,
            ImageLockMode mode = ImageLockMode.ReadWrite,
            PixelFormat fmt = PixelFormat.Format24bppRgb) =>
                src.LockBits(
                    new Rectangle(0, 0, src.Width, src.Height),
                    mode,
                    PixelFormat.Format24bppRgb);

        private static BitmapData LockBits(
            Bitmap src,
            Rectangle rect,
            ImageLockMode mode = ImageLockMode.ReadWrite,
            PixelFormat fmt = PixelFormat.Format24bppRgb) =>
                src.LockBits(rect, mode, fmt);

        private static Rectangle RectangleFromSource(Bitmap src) =>
            new Rectangle(0, 0, src.Width, src.Height);

        private const int BytesPerPixel = 3;

        public unsafe Matrix<U> ToMatrix<U>(Func<Color, U> f)
            where U : struct, IEquatable<U>
        {
            var m = Matrix.Create<U>(this.data.Height, this.data.Width);
            var scan0 = (byte*)this.data.Scan0;
            var stride = this.data.Stride;
            for (var y = 0; y < this.data.Height; y++)
            {
                var row = scan0 + (y * stride);
                for (var x = 0; x < this.data.Width; x++)
                {
                    var bi = x * BytesPerPixel;
                    var gi = bi + 1;
                    var ri = bi + 2;

                    var r = row[ri];
                    var g = row[gi];
                    var b = row[bi];

                    var u = Color.FromArgb(r, g, b);
                    m[y, x] = f(u);
                }
            }

            return m;
        }

        public unsafe void MapInPlace(Func<Color, Color> f)
        {
            var scan0 = (byte*)this.data.Scan0;
            var stride = this.data.Stride;
            for (var y = 0; y < this.data.Height; y++)
            {
                var row = scan0 + (y * stride);
                for (var x = 0; x < this.data.Width; x++)
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
}