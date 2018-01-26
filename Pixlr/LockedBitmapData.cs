namespace Pixlr
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Threading.Tasks;
    using Lina;

    public unsafe class LockedBitmapData : IDisposable
    {
        private bool disposed = false;

        private readonly Bitmap src;

        private readonly BitmapData data;

        private byte* scan0;

        private int stride;

        private LockedBitmapData(Bitmap src, BitmapData data)
        {
            this.src = src;
            this.data = data;
            this.scan0 = (byte*)this.data.Scan0;
            this.stride = this.data.Stride;
        }

        public Color this[int x, int y]
        {
            get => this.At(x, y);
            set => this.At(x, y, value);
        }

        public Color At(int x, int y)
        {
            var row = this.GetRow(y);
            var bi = x * BytesPerPixel;
            var gi = bi + 1;
            var ri = bi + 2;
            return Color.FromArgb(row[ri], row[gi], row[bi]);
        }

        public void At(int x, int y, Color color)
        {
            var row = this.GetRow(y);
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

        private byte* GetRow(int y) => this.scan0 + (y * this.stride);

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

        public Matrix<U> ToMatrix<U>(Func<Color, U> f)
            where U : struct, IEquatable<U>
        {
            var m = Matrix.Create<U>(this.data.Height, this.data.Width);
            Parallel.For(0, this.data.Height, y =>
            {
                var row = this.GetRow(y);
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
            });

            return m;
        }

        public void MapInPlace(Func<Color, Color> f)
        {
            Parallel.For(0, this.data.Height, y =>
            {
                var row = this.GetRow(y);
                for (var x = 0; x < this.data.Width; x++)
                {
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
            });
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