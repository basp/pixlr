// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixlr.Tests;

public class Matrix3x3Tests
{
    private const double epsilon = 1e-6;

    [Fact]
    public void TestSubmatrix()
    {
        var a = new Matrix3x3(
            1, 5, 0,
            -3, 2, 7,
            0, 6, -3);

        var expected = new Matrix2x2(
            -3, 2,
            0, 6);

        var comparer = new Matrix2x2EqualityComparer(epsilon);
        Assert.Equal(expected, a.Submatrix(0, 2), comparer);
    }

    [Fact]
    public void TestCalculateMinorOf3x3Matrix()
    {
        var a = new Matrix3x3(
            3, 5, 0,
            2, -1, -7,
            6, -1, 5);

        var b = a.Submatrix(1, 0);

        Assert.Equal(25, b.GetDeterminant());
        Assert.Equal(25, a.Minor(1, 0));
    }

    [Fact]
    public void TestCalculateCofactorOf3x3Matrix()
    {
        var a = new Matrix3x3(
            3, 5, 0,
            2, -1, -7,
            6, -1, 5);

        Assert.Equal(-12, a.Minor(0, 0));
        Assert.Equal(-12, a.Cofactor(0, 0));
        Assert.Equal(25, a.Minor(1, 0));
        Assert.Equal(-25, a.Cofactor(1, 0));
    }

    [Fact]
    public void TestCalculateDeterminantOf3x3Matrix()
    {
        var a = new Matrix3x3(
            1, 2, 6,
            -5, 8, -4,
            2, 6, 4);

        Assert.Equal(56, a.Cofactor(0, 0));
        Assert.Equal(12, a.Cofactor(0, 1));
        Assert.Equal(-46, a.Cofactor(0, 2));
        Assert.Equal(-196, a.GetDeterminant());
    }
}