namespace Pixlr.Tests;

public class LightTests
{
    [Fact]
    public void PointLightHasPositionAndIntensity()
    {
        var intensity = new Color(1, 1, 1);
        var position = Vector4.CreatePosition(0, 0, 0);
        var light = new PointLight(position, intensity);
        Assert.Equal(intensity, light.Intensity);
        Assert.Equal(position, light.Position);
    }
}