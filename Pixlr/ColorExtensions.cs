namespace Pixlr
{
    using System;
    using System.Drawing;

    public static class ColorExtensions
    {
        public static double Lum(this Color self) =>
            (0.2126 * self.R + 0.7152 * self.G + 0.0722 * self.B) / 256.0;

        public static Color GS(this double i) =>
            Color.FromArgb((byte)(i * 256), (byte)(i * 256), (byte)(i * 256));
    }
}