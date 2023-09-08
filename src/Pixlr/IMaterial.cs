namespace Pixlr;

public interface IMaterial
{
    Color Lighting(PointLight light, Interaction intr);

    Color GetColor(
        PointLight light,
        Vector4 position,
        Vector4 eyev,
        Vector4 normalv,
        bool shadow = false);
}