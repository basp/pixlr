namespace Pixlr
{
    using System.Drawing;

    public static class StringExtensions
    {
        public static Bitmap LoadAsBitmap(this string self) =>
            (Bitmap)Bitmap.FromFile(self);
    }
}