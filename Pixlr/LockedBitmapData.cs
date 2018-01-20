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

        public Color this[int row, int column]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Color At(int row, int column)
        {
            throw new NotImplementedException();
        }

        public void At(int row, int column, Color color)
        {
            throw new NotImplementedException();
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

        private unsafe Matrix<U> MapToMatrix<U>(Func<Color, U> f)
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
                    // Note that order is BGR (blame Microsoft)
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