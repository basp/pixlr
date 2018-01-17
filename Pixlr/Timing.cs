namespace Pixlr
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    public static class Utils
    {
        public static double Average(Action action, int n = 100) =>
            Enumerable.Range(0, n).Select(_ => Time(action)).Average();

        private static long Time(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}