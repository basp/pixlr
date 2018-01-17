namespace Pixlr
{
    using System;
    using System.Drawing;

    public class Unsafe
    {
        private readonly Bitmap src;

        public Unsafe(Bitmap src)
        {
            this.src = src;
        }

        public void MapInPlace(Func<Color, Color> f)
        {
            throw new NotImplementedException();
        }
    }

    public static class BitmapExtensions
    {
        public static Unsafe Unsafe(this Bitmap self) => new Unsafe(self);
    }
}
