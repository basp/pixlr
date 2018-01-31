namespace Pixlr.Cmd
{
    using System;
    using System.Linq;

    public static class UriExtensions
    {
        public static string Normalize(this Uri uri)
        {
            var nsegments = uri.Segments.Count();
            var lastSegment = uri.Segments.Last();
            if (lastSegment.Contains("."))
            {
                var leftPart = uri.GetLeftPart(UriPartial.Authority);
                var rightPart = string.Concat(uri.Segments.Take(nsegments - 1));
                return string.Concat(leftPart, rightPart);
            }

            return uri.OriginalString;
        }
    }
}
