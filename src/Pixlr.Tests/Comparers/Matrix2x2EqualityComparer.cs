// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixlr.Tests.Comparers;

using System;

internal class Matrix2x2EqualityComparer : ApproxEqualityComparer<Matrix2x2>
{
    public Matrix2x2EqualityComparer(double epsilon = 0)
        : base(epsilon)
    {
    }

    public override bool Equals(Matrix2x2 x, Matrix2x2 y)
    {
        for (var j = 0; j < 2; j++)
        {
            for (var i = 0; i < 2; i++)
            {
                if (!this.ApproxEqual(x[i, j], y[i, j]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int GetHashCode(Matrix2x2 obj) =>
        HashCode.Combine(
            obj[0, 0], obj[0, 1],
            obj[1, 0], obj[1, 1]);
}