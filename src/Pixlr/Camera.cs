namespace Pixlr;

public class Camera
{
    private readonly double halfWidth;
    private readonly double halfHeight;
    
    public Camera(int width, int height, double fov)
    {
        var halfView = Math.Tan(fov / 2);
        var aspect = (double)width / height;
        if (aspect >= 1)
        {
            this.halfWidth = halfView;
            this.halfHeight = halfView / aspect;
        }
        else
        {
            this.halfWidth = halfView * aspect;
            this.halfHeight = halfView;
        }

        this.PixelSize = (this.halfWidth * 2) / width;
        this.Resolution = (width, height);
    }
    
    public (int, int) Resolution { get; }
    
    public double PixelSize { get; }

    public Transform Transform { get; init; } = new(Matrix4x4.Identity);

    public Ray GenerateRay(int px, int py)
    {
        var xOffset = (px + 0.5) * this.PixelSize;
        var yOffset = (py + 0.5) * this.PixelSize;
        var worldX = this.halfWidth - xOffset;
        var worldY = this.halfHeight - yOffset;
        var pixel = Vector4.Transform(
            Vector4.CreatePosition(worldX, worldY, -1),
            this.Transform.Inverse);
        var origin = Vector4.Transform(
            Vector4.CreatePosition(0, 0, 0),
            this.Transform.Inverse);
        var direction = Vector4.Normalize(pixel - origin);
        return new Ray(origin, direction);
    }

    public Pixmap Render(World world)
    {
        var (width, height) = this.Resolution;
        var image = new Pixmap(width, height);
        for (var y = 0; y < height - 1; y++)
        {
            for (var x = 0; x < width - 1; x++)
            {
                var ray = this.GenerateRay(x, y);
                var color = world.GetColor(ray);
                image[x, y] = color;
            }
        }

        return image;
    }
}