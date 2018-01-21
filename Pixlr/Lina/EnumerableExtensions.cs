namespace Pixlr.Lina
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static Vector<T> ToVector<T>(this IEnumerable<T> self)
            where T : struct, IEquatable<T>
            => Vector.Build<T>().Dense(self.ToArray());
    }
}