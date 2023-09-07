namespace Pixlr;

public class Transform
{
    public Transform(Matrix4x4 matrix, Matrix4x4 inverse)
    {
        this.Matrix = matrix;
        this.Inverse = inverse;
    }
    
    public Transform(Matrix4x4 matrix)
    {
        this.Matrix = matrix;
        if (!Matrix4x4.Invert(matrix, out var inv))
        {
            inv = new Matrix4x4(
                double.NaN, double.NaN, double.NaN, double.NaN,
                double.NaN, double.NaN, double.NaN, double.NaN,
                double.NaN, double.NaN, double.NaN, double.NaN,
                double.NaN, double.NaN, double.NaN, double.NaN);
        }
        this.Inverse = inv;
    }
    
    public Matrix4x4 Matrix { get; }
    
    public Matrix4x4 Inverse { get; }
}