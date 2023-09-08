namespace Pixlr;

public interface IMaterial
{
    Color GetColor(
        PointLight light,
        Vector4 position,
        Vector4 eyev,
        Vector4 normalv);
}