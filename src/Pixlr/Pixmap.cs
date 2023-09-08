namespace Pixlr;

public class Pixmap
{
    private readonly Color[] buf;
    
    public Pixmap(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this.buf = new Color[width * height];
    }

    public Color this[int x, int y]
    {
        get => this.buf[(y * this.Width) + x];
        set => this.buf[(y * this.Width) + x] = value;
    }
    
    public int Width { get; init; }

    public int Height { get; init; }

    public void Save(TextWriter writer)
    {
        writer.WriteLine("P3");
        writer.WriteLine("{0} {1}", this.Width, this.Height);
        writer.WriteLine("255");
        for (var j = 0; j < this.Height; j++)
        {
            for (var i = 0; i < this.Width; i++)
            {
                this.WriteColor(writer, this[i, j]);
            }
        }
    }
        
    private void WriteColor(TextWriter w, Color color)
    {
        var (r, g, b) = color;
        var intensity = new Interval(0, 0.999f);
        w.WriteLine(
            "{0} {1} {2}",
            (int)(256 * intensity.Clamp(r)),
            (int)(256 * intensity.Clamp(g)),
            (int)(256 * intensity.Clamp(b)));
    }
}