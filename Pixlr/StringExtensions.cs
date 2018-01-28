namespace Pixlr
{
    using System.Drawing;
    using System.Text;

    public static class StringExtensions
    {
        public static Bitmap LoadAsBitmap(this string self) =>
            (Bitmap)Bitmap.FromFile(self);

        public static string Repeat(this string self, int n)
        {
            var sb = new StringBuilder();
            for(var i = 0; i < n; i++)
            {
                sb.Append(self);
            }

            return sb.ToString();
        }
    }
}