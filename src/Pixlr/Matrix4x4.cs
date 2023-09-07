// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixlr;

using System.Linq;

/// <summary>
/// 4x4 matrix of <c>double</c> values.
/// </summary>
public class Matrix4x4
{
    private readonly double[] data;

    public Matrix4x4(double v)
    {
        this.data = new[]
        {
            v, v, v, v,
            v, v, v, v,
            v, v, v, v,
            v, v, v, v,
        };
    }

    public Matrix4x4(
        double m00, double m01, double m02, double m03,
        double m10, double m11, double m12, double m13,
        double m20, double m21, double m22, double m23,
        double m30, double m31, double m32, double m33)
    {
        this.data = new[]
        {
            m00, m01, m02, m03,
            m10, m11, m12, m13,
            m20, m21, m22, m23,
            m30, m31, m32, m33,
        };
    }

    public static Matrix4x4 Identity =>
        new(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

    public double this[int row, int col]
    {
        get => this.data[(row * 4) + col];
        set => this.data[(row * 4) + col] = value;
    }

    public static Matrix4x4 CreateRotationX(double radians) =>
        new(
            1, 0, 0, 0,
            0, Math.Cos(radians), -Math.Sin(radians), 0,
            0, Math.Sin(radians), Math.Cos(radians), 0,
            0, 0, 0, 1);

    public static Matrix4x4 CreateRotationY(double radians) =>
        new(
            Math.Cos(radians), 0, Math.Sin(radians), 0,
            0, 1, 0, 0,
            -Math.Sin(radians), 0, Math.Cos(radians), 0,
            0, 0, 0, 1);

    public static Matrix4x4 CreateRotationZ(double radians) =>
        new(
            Math.Cos(radians), -Math.Sin(radians), 0, 0,
            Math.Sin(radians), Math.Cos(radians), 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

    public static Matrix4x4 CreateScale(
        double xScale,
        double yScale,
        double zScale) =>
        new(
            xScale, 0.0, 0.0, 0.0,
            0.0, yScale, 0.0, 0.0,
            0.0, 0.0, zScale, 0.0,
            0.0, 0.0, 0.0, 1.0);

    public static Matrix4x4 CreateShear(
        double xy,
        double xz,
        double yx,
        double yz,
        double zx,
        double zy) =>
        new(
            1, xy, xz, 0,
            yx, 1, yz, 0,
            zx, zy, 1, 0,
            0, 0, 0, 1);

    public static Matrix4x4 CreateTranslation(
        double xPosition,
        double yPosition,
        double zPosition) =>
        new(
            1.0, 0.0, 0.0, xPosition,
            0.0, 1.0, 0.0, yPosition,
            0.0, 0.0, 1.0, zPosition,
            0.0, 0.0, 0.0, 1.0);

    /// <summary>
    /// Calculates the determinant of this matrix using Laplace expansion.
    /// </summary>
    public double GetDeterminant() =>
        (this[0, 0] * this.Cofactor(0, 0)) +
        (this[0, 1] * this.Cofactor(0, 1)) +
        (this[0, 2] * this.Cofactor(0, 2)) +
        (this[0, 3] * this.Cofactor(0, 3));

    public static Matrix4x4 Multiply(Matrix4x4 a, Matrix4x4 b)
    {
        var m = new Matrix4x4(0);
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                m[i, j] =
                    (a[i, 0] * b[0, j]) +
                    (a[i, 1] * b[1, j]) +
                    (a[i, 2] * b[2, j]) +
                    (a[i, 3] * b[3, j]);
            }
        }

        return m;
    }

    public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
    {
        if (!matrix.IsInvertible())
        {
            result = null;
            return false;
        }

        result = new Matrix4x4(0);
        var d = matrix.GetDeterminant();
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                result[j, i] = matrix.Cofactor(i, j) / d;
            }
        }

        return true;
    }

    public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b) =>
        Multiply(a, b);

    public static Matrix4x4 Transpose(Matrix4x4 a)
    {
        var m = new Matrix4x4(0);
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                m[i, j] = a[j, i];
            }
        }

        return m;
    }

    public override string ToString() =>
        $"({string.Join(", ", this.data)})";
}

public static class Matrix4x4Extensions
{
    public static Matrix4x4 RotateX(
        this Matrix4x4 self,
        double radians) =>
        Matrix4x4.CreateRotationX(radians) * self;

    public static Matrix4x4 RotateY(
        this Matrix4x4 self,
        double radians) =>
        Matrix4x4.CreateRotationY(radians) * self;

    public static Matrix4x4 RotateZ(
        this Matrix4x4 self,
        double radians) =>
        Matrix4x4.CreateRotationZ(radians) * self;
    
    public static Matrix4x4 Scale(
        this Matrix4x4 self,
        double scaleX,
        double scaleY,
        double scaleZ) =>
        Matrix4x4.CreateScale(
            scaleX,
            scaleY,
            scaleZ) * self;

    public static Matrix4x4 Translate(
        this Matrix4x4 self,
        double positionX,
        double positionY,
        double positionZ) =>
        Matrix4x4.CreateTranslation(
            positionX,
            positionY,
            positionZ) * self;

    internal static bool IsInvertible(this Matrix4x4 a) =>
        a.GetDeterminant() != 0;

    // This creates a new 3x3 matrix from a 4x4 matrix by dropping one
    // row and one column. The row and column to be dropped are specified
    // by the `dropRow` and `dropCol` arguments respectively.
    //
    // Ex. (dropRow = 2, dropCol = 1)
    // ---
    // A B C D  =>  A | C D  =>  A C D
    // E F G H      E | G H      E G H
    // I J K L      - + - -      M O P
    // M N O P      M | O P
    internal static Matrix3x3 Submatrix(this Matrix4x4 a, int dropRow, int dropCol)
    {
        var rows = Enumerable.Range(0, 4) // 0, 1, 2, 3
            .Where(x => x != dropRow)
            .ToArray();

        var cols = Enumerable.Range(0, 4)
            .Where(x => x != dropCol)
            .ToArray();

        var m = new Matrix3x3(0);
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                m[i, j] = a[rows[i], cols[j]];
            }
        }

        return m;
    }

    internal static double Cofactor(this Matrix4x4 a, int row, int col) =>
        (row + col) % 2 == 0 ? a.Minor(row, col) : -a.Minor(row, col);

    // For more information on minors, cofactors, etc. and how the
    // determinant is calculated see:
    //
    // * https://en.wikipedia.org/wiki/Minor_(linear_algebra)
    // * https://en.wikipedia.org/wiki/Laplace_expansion
    private static double Minor(this Matrix4x4 a, int row, int col) =>
        a.Submatrix(row, col).GetDeterminant();
}