// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pixlr.Tests")]

namespace Pixlr;

using System.Linq;

internal class Matrix3x3
{
    private readonly double[] data;

    public Matrix3x3(double v)
    {
        this.data = new[]
        {
            v, v, v,
            v, v, v,
            v, v, v,
        };
    }

    public Matrix3x3(
        double m00, double m01, double m02,
        double m10, double m11, double m12,
        double m20, double m21, double m22)
    {
        this.data = new[]
        {
            m00, m01, m02,
            m10, m11, m12,
            m20, m21, m22,
        };
    }

    public double this[int row, int col]
    {
        get => this.data[(row * 3) + col];
        set => this.data[(row * 3) + col] = value;
    }
    
    public double GetDeterminant() =>
        (this[0, 0] * this.Cofactor(0, 0)) +
        (this[0, 1] * this.Cofactor(0, 1)) +
        (this[0, 2] * this.Cofactor(0, 2));
}

internal static class Matrix3x3Extensions
{
    internal static Matrix2x2 Submatrix(this Matrix3x3 a, int dropRow, int dropCol)
    {
        var rows = Enumerable.Range(0, 3)
            .Where(x => x != dropRow)
            .ToArray();

        var cols = Enumerable.Range(0, 3)
            .Where(x => x != dropCol)
            .ToArray();

        var m = new Matrix2x2(0);
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                m[i, j] = a[rows[i], cols[j]];
            }
        }

        return m;
    }

    internal static double Minor(this Matrix3x3 a, int row, int col) =>
        a.Submatrix(row, col).GetDeterminant();

    internal static double Cofactor(this Matrix3x3 a, int row, int col) =>
        (row + col) % 2 == 0 ? a.Minor(row, col) : -a.Minor(row, col);
}