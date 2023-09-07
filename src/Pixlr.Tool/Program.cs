using Pixlr;

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

using var stream = File.OpenWrite(@".\test.ppm");
using var writer = new StreamWriter(stream);
ppm.Save(writer);