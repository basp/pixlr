// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixlr.Tests.Comparers;

using System;

internal class Matrix3x3EqualityComparer : ApproxEqualityComparer<Matrix3x3>
{
    public Matrix3x3EqualityComparer(double epsilon = 0)
        : base(epsilon)
    {
    }

    public override bool Equals(Matrix3x3 x, Matrix3x3 y)
    {
        for (var j = 0; j < 3; j++)
        {
            for (var i = 0; i < 3; i++)
            {
                if (!this.ApproxEqual(x[i, j], y[i, j]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int GetHashCode(Matrix3x3 obj) =>
        HashCode.Combine(
            HashCode.Combine(
                obj[0, 0], obj[0, 1], obj[0, 2],
                obj[1, 0], obj[1, 1], obj[1, 2]),
            HashCode.Combine(
                obj[2, 0], obj[2, 1], obj[2, 2]));
}