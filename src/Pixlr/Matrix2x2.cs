// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixlr;

internal class Matrix2x2
{
    private readonly double[] data;

    public Matrix2x2(double v)
    {
        this.data = new[]
        {
            v, v,
            v, v,
        };
    }

    public Matrix2x2(
        double m00, double m01,
        double m10, double m11)
    {
        this.data = new[]
        {
            m00, m01,
            m10, m11,
        };
    }

    public double this[int row, int col]
    {
        get => this.data[(row * 2) + col];
        set => this.data[(row * 2) + col] = value;
    }

    public double GetDeterminant() =>
        (this[0, 0] * this[1, 1]) - (this[0, 1] * this[1, 0]);

    public override string ToString() =>
        $"({string.Join(", ", this.data)})";
}