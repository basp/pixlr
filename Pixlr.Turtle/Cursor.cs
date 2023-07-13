namespace Pixlr.Turtle;

using System.Drawing;
using Linie;

public interface ICursor
{
    ICursor Forward(double d);

    ICursor Move(double d);

    ICursor Turn(double angle);

    ICursor Resize(double s);
}

public interface ICanvas
{
    void DrawLine(int x1, int y1, int x2, int y2);
}

public class BitmapAdapter : ICanvas
{
    private readonly Pen pen = new Pen(Brushes.Black, 1f);

    private readonly Bitmap bmp;

    private readonly Graphics gfx;

    public BitmapAdapter(Bitmap bmp)
    {
        this.bmp = bmp;
        this.gfx = Graphics.FromImage(bmp);
    }

    public void DrawLine(int x1, int y1, int x2, int y2)
    {
        this.gfx.DrawLine(this.pen, x1, y1, x2, y2);
    }
}

public class Cursor : ICursor
{
    private readonly ICanvas canvas;

    private Vector4 p = Vector4.CreatePosition(0, 0, 0);

    private Vector4 w = Vector4.CreateDirection(0, 1, 0);

    public Cursor(ICanvas canvas)
    {
        this.canvas = canvas;
    }

    public ICursor Forward(double d)
    {
        var x1 = (int)(Math.Round(this.p.X));
        var y1 = (int)(Math.Round(this.p.Y));
        this.p = this.p + (d * this.w);
        var x2 = (int)(Math.Round(this.p.X));
        var y2 = (int)(Math.Round(this.p.Y));
        this.canvas.DrawLine(x1, y1, x2, y2);
        return this;
    }

    public ICursor Move(double d)
    {
        this.p = this.p + (d * this.w);
        return this;
    }

    public ICursor Turn(double angle)
    {
        var m = Affine.RotateZ(angle);
        this.w = m * this.w;
        return this;
    }

    public ICursor Resize(double s)
    {
        this.w = s * this.w;
        return this;
    }
}