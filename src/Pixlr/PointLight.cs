namespace Pixlr;

public class PointLight
{
    public PointLight(Vector4 position, Color intensity)
    {
        this.Position = position;
        this.Intensity = intensity;
    }

    public Vector4 Position { get; }

    public Color Intensity { get; }
}