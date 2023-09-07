namespace Pixlr;

public static class Utils
{
    public static double Lerp(double v1, double v2, double t) =>
        (1 - t) * v1 + t * v2;
}