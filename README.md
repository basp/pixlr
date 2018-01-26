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
* Although working with matrices is *very* nice, this will consume a lot of memory. There's the original bitmap, the matrix (of equal size to the bitmap dimensions) that has (probably 64-bit) doubles and also the result bitmap that have to reside in memory at the same time for a short while (i.e. it's not inconceivable that you run into an `OutOfMemoryException` when working with large images).

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

### lina
TODO

### convolutions
TODO