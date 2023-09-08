namespace Pixlr.Tests;

public class WorldTests
{
    private readonly World DefaultWorld = new World
    {
        Objects = new List<IShape>
        {
            new Sphere()
            {
                Material = new Material
                {
                    Color = new Color(0.8, 1.0, 0.6),
                    Diffuse = 0.7,
                    Specular = 0.2,
                },
            },
            new Sphere()
            {
                Transform = new Transform(
                    Matrix4x4.CreateScale(0.5, 0.5, 0.5)),
            },
        },
        Lights = new List<PointLight>
        {
            new(
                Vector4.CreatePosition(-10, 10, -10),
                new Color(1, 1, 1)),
        },
    };

    [Fact]
    public void CreatingAWorld()
    {
        var world = new World();
        Assert.Empty(world.Lights);
        Assert.Empty(world.Objects);
    }

    [Fact]
    public void TheDefaultWorld()
    {
        var world = this.DefaultWorld;

        var s1 = world.Objects[0];
        var s2 = world.Objects[1];

        var light = world.Lights[0];
        
        var comparer = new Matrix4x4EqualityComparer(1e-6);

        Assert.Equal(Vector4.CreatePosition(-10, 10, -10), light.Position);
        Assert.Equal(new Color(1, 1, 1), light.Intensity);
        
        Assert.Equal(new Color(0.8, 1, 0.6), s1.Material.Color);
        Assert.Equal(0.7, s1.Material.Diffuse);
        Assert.Equal(0.2, s1.Material.Specular);

        Assert.Equal(
            Matrix4x4.CreateScale(0.5, 0.5, 0.5),
            s2.Transform.Matrix,
            comparer);
    }

    [Fact]
    public void IntersectWorldWithRay()
    {
        var w = this.DefaultWorld;
        var r = new Ray(
            Vector4.CreatePosition(0, 0, -5),
            Vector4.CreateDirection(0, 0, 1));
        var xs = w
            .IntersectAll(r)
            .Order()
            .ToList();
        Assert.Equal(4, xs.Count);
        Assert.Equal(4, xs[0].T);
        Assert.Equal(4.5, xs[1].T);
        Assert.Equal(5.5, xs[2].T);
        Assert.Equal(6, xs[3].T);
    }
    
    [Fact]
    public void ShadingAnIntersection()
    {
        var w = this.DefaultWorld;
        var r = new Ray(
            Vector4.CreatePosition(0, 0, -5),
            Vector4.CreateDirection(0, 0, 1));
        var shape = w.Objects[0];
        var i = new Intersection(4, shape);
        var intr = Interaction.FromIntersection(i, r);
        var actual = w.GetColor(intr);
        var expected = new Color(0.38066, 0.47583, 0.2855); 
        var comparer = new ColorEqualityComparer(1e-5);
        Assert.Equal(expected, actual, comparer); 
    }

    [Fact]
    public void ShadingAnIntersectionFromTheInside()
    {
        var w = this.DefaultWorld;
        w.Lights.Clear();
        w.Lights.Add(new PointLight(
            Vector4.CreatePosition(0, 0.25, 0),
            new Color(1, 1, 1)));
        var r = new Ray(
            Vector4.CreatePosition(0, 0, 0),
            Vector4.CreateDirection(0, 0, 1));
        var shape = w.Objects[1];
        var i = new Intersection(0.5, shape);
        var intr = Interaction.FromIntersection(i, r);
        var actual = w.GetColor(intr);
        var expected = new Color(0.90498, 0.90498, 0.90498);
        var comparer = new ColorEqualityComparer(1e-5);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void TheColorWhenTheRayMisses()
    {
        var w = this.DefaultWorld;
        var r = new Ray(
            Vector4.CreatePosition(0, 0, -5),
            Vector4.CreateDirection(0, 1, 0));
        var actual = w.GetColor(r);
        var expected = new Color(0, 0, 0);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TheColorWhenTheRayHits()
    {
        var w = this.DefaultWorld;
        var r = new Ray(
            Vector4.CreatePosition(0, 0, -5),
            Vector4.CreateDirection(0, 0, 1));
        var actual = w.GetColor(r);
        var expected = new Color(0.38066, 0.47583, 0.2855);
        var comparer = new ColorEqualityComparer(1e-5);
        Assert.Equal(expected, actual, comparer);
    }

    [Fact]
    public void TheColorWithAnIntersectionBehindTheRay()
    {
        var w = this.DefaultWorld;
        var outer = w.Objects[0];
        var inner = w.Objects[1];
        outer.Material = outer.Material with { Ambient = 1 };
        inner.Material = inner.Material with { Ambient = 1 };
        var r = new Ray(
            Vector4.CreatePosition(0, 0, 0.75),
            Vector4.CreateDirection(0, 0, -1));
        var actual = w.GetColor(r);
        Assert.Equal(inner.Material.Color, actual);
    }
}