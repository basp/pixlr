using Pixlr;

using Matrix4x4 = Pixlr.Matrix4x4;
using Vector4 = Pixlr.Vector4;

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

static void Example3()
{
    var canvasPixels = 200;
    var wallZ = 10;
    var wallSize = 7.0;
    var pixelSize = wallSize / canvasPixels;
    var half = wallSize / 2;
    var rayOrigin = Vector4.CreatePosition(0, 0, -5);
    var ppm = new Pixmap(canvasPixels, canvasPixels);
    
    var m = Matrix4x4
        .Identity;
    
    var shape = new Sphere()
    {
        Transform = new Transform(m),
        Material = new Material
        {
            Color = new Color(1, 0.2, 1),
        },
    };

    var light = new PointLight(
        Vector4.CreatePosition(-10, 10, -10),
        new Color(1, 1, 1));

    for (var y = 0; y < canvasPixels - 1; y++)
    {
        var worldY = half - pixelSize * y;
        for (var x = 0; x < canvasPixels - 1; x++)
        {
            var worldX = -half + pixelSize * x;
            var position = Vector4.CreatePosition(worldX, worldY, wallZ);
            var rayDirection = Vector4.Normalize(position - rayOrigin); 
            var r = new Ray(rayOrigin, rayDirection); 
            
            var xs = shape.IntersectAll(r);
            if (!xs.TryGetHit(out var i))
            {
                continue;
            }
            
            var p = r.GetPosition(i.T);
            var color = i.Obj.Material.GetColor(
                light,
                p,
                Vector4.Negate(r.Direction),
                i.Obj.GetNormal(p));
            
            ppm[x, y] = color;
        }
    }
    
    using var stream = File.OpenWrite($@".\{nameof(Example3)}.ppm");
    using var writer = new StreamWriter(stream);
    ppm.Save(writer);
}

static void Example4()
{
    var floor = new Sphere()
    {
        Transform = new Transform(
            Matrix4x4.CreateScale(10, 0.01, 10)),
        Material = new Material()
        {
            Color = new Color(1, 0.9, 0.9),
            Specular = 0,
        },
    };

    var leftWall = new Sphere()
    {
        Transform = new Transform(
            Matrix4x4
                .Identity
                .Scale(10, 0.01, 10)
                .RotateX(Math.PI / 2)
                .RotateY(-Math.PI / 4)
                .Translate(0, 0, 5)),
        Material = floor.Material,
    };

    var rightWall = new Sphere()
    {
        Transform = new Transform(
            Matrix4x4
                .Identity
                .Scale(10, 0.01, 10)
                .RotateX(Math.PI / 2)
                .RotateY(Math.PI / 4)
                .Translate(0, 0, 5)),
        Material = floor.Material,
    };

    var middle = new Sphere()
    {
        Transform = new Transform(
            Matrix4x4.CreateTranslation(-0.5, 1, 0.5)),
        Material = new Material()
        {
            Color = new Color(0.1, 1, 0.5),
            Diffuse = 0.7,
            Specular = 0.3,
        },
    };

    var right = new Sphere()
    {
        Transform = new Transform(
            Matrix4x4
                .Identity
                .Scale(0.5, 0.5, 0.5)
                .Translate(1.5, 0.5, -0.5)),
        Material = new Material()
        {
            Color = new Color(0.5, 1, 0.1),
            Diffuse = 0.7,
            Specular = 0.3,
        },
    };

    var left = new Sphere()
    {
        Transform = new Transform(
            Matrix4x4
                .Identity
                .Scale(0.33, 0.33, 0.33)
                .Translate(-1.5, 0.33, -0.75)),
        Material = new Material()
        {
            Color = new Color(1, 0.8, 0.1),
            Diffuse = 0.7,
            Specular = 0.3,
        },
    };

    var light = new PointLight(
        Vector4.CreatePosition(-10, 10, -10),
        new Color(1, 1, 1));

    var camera = new Camera(200, 100, Math.PI / 3)
    {
        Transform = new Transform(
            Matrix4x4.CreateLookAt(
                Vector4.CreatePosition(0, 1.5, -5),
                Vector4.CreatePosition(0, 1, 0),
                Vector4.CreateDirection(0, 1, 0))),
    };

    var world = new World()
    {
        Objects = new List<IShape>
        {
            floor, 
            leftWall, 
            rightWall, 
            middle, 
            right, 
            left,   
        },
        Lights = new List<PointLight>
        {
            light,  
        },
    };

    var image = camera.Render(world);
    using var stream = File.OpenWrite($@".\{nameof(Example4)}.ppm");
    using var writer = new StreamWriter(stream);
    image.Save(writer);
}

Example4();