using Pixlr;

static void Example1()
{
    var ppm = new Pixmap(100, 100);
    var twelveOClock = Vector4.CreatePosition(0, 0, 1);
    var angle = Math.PI / 6;
    var scaleX = 3.0 / 8 * ppm.Width;
    var scaleZ = 3.0 / 8 * ppm.Height;

    for (var h = 0; h < 12; h++)
    {
        var radians = h * angle;
        var transform = Matrix4x4
            .Identity
            .RotateY(radians)
            .Scale(scaleX, 1, scaleZ);
        var p = Vector4.Transform(twelveOClock, transform);
        var x = 50 + (int)p.X;
        var y = 50 + (int)p.Z;
        ppm[x, y] = new Color(1.0, 1.0, 1.0);
    }

    using var stream = File.OpenWrite(@$".\{nameof(Example1)}.ppm");
    using var writer = new StreamWriter(stream);
    ppm.Save(writer);    
}

static void Example2()
{
    var canvasPixels = 100;
    var wallZ = 10;
    var wallSize = 7.0;
    var pixelSize = wallSize / canvasPixels;
    var half = wallSize / 2;
    var rayOrigin = Vector4.CreatePosition(0, 0, -5);
    var ppm = new Pixmap(canvasPixels, canvasPixels);
    var color = new Color(1, 0, 0);
    var m = Matrix4x4
        .Identity
        .Scale(1, 0.5, 1)
        .RotateZ(Math.PI / 4);
    var shape = new Sphere()
    {
        Transform = new Transform(m),
    };
    for (var y = 0; y < canvasPixels - 1; y++)
    {
        var worldY = half - pixelSize * y;
        for (var x = 0; x < canvasPixels - 1; x++)
        {
            var worldX = -half + pixelSize * x;
            var position = Vector4.CreatePosition(worldX, worldY, wallZ);
            var r = new Ray(
                rayOrigin, 
                Vector4.Normalize(position - rayOrigin));
            var xs = shape.IntersectAll(r);
            if (xs.TryGetHit(out var i))
            {
                ppm[x, y] = color;
            }
        }
    }

    using var stream = File.OpenWrite($@".\{nameof(Example2)}.ppm");
    using var writer = new StreamWriter(stream);
    ppm.Save(writer);
}

Example2();
