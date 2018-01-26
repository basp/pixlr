## pixlr
Fast bitmap operations.

### why
The whole reason why this thing exists is because I wanted to do operations on bitmaps that might involve matrices and vectors as well. And as I like to do things interactively I wanted to make the API simple and functionally flavored without making too much compromises on speed.

The desire to use this interactively drove the need for a functional API. And the functional API made it a lot easier to optimize *embarrassingly parallel* workloads so we can get a decent performance without comprimising too much on readability.

### quick start
The example below creates a `Matrix<double>` from a `LockedBitmapData` instance using the `Lum` extension method (defined on `Color` values by **Pixlr**) to get a grayscale value for each RGB color in the source image. Next, we call `ToBitmap` to convert the `Matrix<double>` (luminance map) back to a bitmap again, this time using the `GS` function (defined on `double` values, also by **Pixlr**) to get back a `Color` instance.
```
using (var bmp = (Bitmap)Bitmap.FromFile(path))
using (var data = bmp.Lock())
{
    var M = data.ToMatrix(c => c.Lum());
    M.ToBitmap(v => v.GS()).Dump();
}
```
There's a few things that are important to note about the above code:
* This assumes that you're evaluating **Pixlr** in **LINQPad** otherwise the `Dump` method will probably not be available and you'll most likely have to write the bitmap to disk or show it in a window in order to inspect the results.
* Although working with matrices is *very* nice, this will consume **a lot of memory**. Eventually, there's the original bitmap, the matri(x)(ces) (of equal size to the bitmap dimensions) that have (probably **64-bit**) doubles and also the result bitmap (again, sized to the original bitmap) that have to reside in memory all at the same time for at least a short while (i.e. it's not inconceivable that you run into an `OutOfMemoryException` when casually working with large images and matrices).
* It's quite fast.

### mapping in place
If converting to and from matrices is too slow or too memory consuming, you can also *map in place* directly into the bitmap memory.
```
using (var bmp = (Bitmap)Bitmap.FromFile(path))
using (var data = bmp.Lock())
{
    data.MapInPlace(f => f.Lum().GS());
    bmp.Dump();
}
```

The reason we can do this is because we can map each pixel's `Color` value directly to another `Color` value. This is absolutely the best way to go if you have such a simple operation (like converting an image to colorscale) but for more advanced things you can't *mutilate* the original source. 

If we want to preserve the original bitmap, we probably have to prepare an *output buffer* in the form of anoter `Bitmap` instance and place our results into there, keeping the original bitmap intact. In this case, we just want to enumerate over the pixels as fast as possible and do something, *whatever*, and place them in our own custom memory space.

### just enumerating
If you just want to enumerate over the pixels that is possible too. Below is an example of just raw looping over the pixels and copying the result into another `LockedBitmapData` instance (so not to interfere with the original sampling).
```
using (var bmp = (Bitmap)Bitmap.FromFile(path))
using (var data = bmp.Lock(ImageLockMode.ReadOnly))
{
    using (var @out = new Bitmap(data.Width, data.Height))
    {
        using (var @odata = @out.Lock(ImageLockMode.WriteOnly))
        {
            Parallel.For(0, data.Height, y =>
            {
                for (var x = 0; x < data.Width; x++)
                {
                    @odata[x, y] = data[x, y].Lum().GS();
                }
            });
        }
        
        @out.Dump();
    }
}
```

This technique can be useful if you need to optimize **convolutions** (see below) since you're not mapping directly into the source (not a good thing when you rely on *neighboring* values). For even a little bit more efficiency we can even specify the `ImageLockMode` properties. In this case we obviously specify `ReadOnly` for the source and `WriteOnly` for the target bitmap.

And yes, the nested `using` blocks are a bit *fugly* indeed.

### lina
To be honest, I didn't really wanna create my own `Vector<T>` and `Matrix<T>` classes but in the end it turned out that they were faster than any alternative I tried... So I decided to keep them. They are pretty bare but versatile. However, they eat up a lot of memory (due to being dense by nature) so use them if you want or otherwise just use `MapInPlace` instead.

### convolutions
TODO