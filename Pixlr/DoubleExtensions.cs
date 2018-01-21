namespace Pixlr
{
    using System;
    using System.Drawing;

    public static class DoubleExtensions
    {
        public static double Inv(this double self) => 1.0 - self;

        public static Color GS(this double self) =>
            Color.FromArgb(
                (byte)(self * byte.MaxValue), 
                (byte)(self * byte.MaxValue), 
                (byte)(self * byte.MaxValue));
    }
}