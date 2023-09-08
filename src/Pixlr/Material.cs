namespace Pixlr;

public record Material(
    Color Color,
    double Ambient,
    double Diffuse,
    double Specular,
    double Shininess) : IMaterial
{
    public Material()
        : this(
            new Color(1, 1, 1),
            0.1,
            0.9,
            0.9,
            200.0)
    {
    }

    public Color Lighting(PointLight light, Interaction intr) =>
        this.GetColor(
            light,
            intr.Point,
            intr.Eye,
            intr.Normal);

    public Color GetColor(
        PointLight light,
        Vector4 position,
        Vector4 eyev,
        Vector4 normalv,
        bool shadow = false)
    {
        var effectiveColor = Color.Multiply(this.Color, light.Intensity);
        var lightv = Vector4.Normalize(light.Position - position);
        var lightDotNormal = Vector4.Dot(lightv, normalv);

        var ambient = Color.Multiply(effectiveColor, this.Ambient);

        if (shadow)
        {
            return ambient;
        }
        
        Color diffuse;
        Color specular;

        if (lightDotNormal < 0)
        {
            diffuse = new(0, 0, 0);
            specular = new(0, 0, 0);
        }
        else
        {
            diffuse = Color.Multiply(effectiveColor, this.Diffuse * lightDotNormal);
            var reflectv = Vector4.Reflect(Vector4.Negate(lightv), normalv);
            var reflectDotEye = Vector4.Dot(reflectv, eyev);
            if (reflectDotEye <= 0)
            {
                specular = new(0, 0, 0);
            }
            else
            {
                var factor = Math.Pow(reflectDotEye, this.Shininess);
                specular = Color.Multiply(light.Intensity, this.Specular * factor);
            }
        }

        return Color.Add(
            Color.Add(ambient, diffuse),
            specular);
    }
}