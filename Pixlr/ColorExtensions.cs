namespace Pixlr
{
    using System;
    using System.Drawing;

    public static class ColorExtensions
    {
        public static double Lum(this Color self) =>
            (0.2126 * self.R + 0.7152 * self.G + 0.0722 * self.B) / 256;

        public static Color Inv(this Color self) =>
            Color.FromArgb(
                byte.MaxValue - self.R, 
                byte.MaxValue - self.R, 
                byte.MaxValue - self.R);
    }
}