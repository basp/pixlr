## pixlr
Fast bitmap operations.

### quick start
Grab a bitmap (an image) and put it somewhere where you can remember this. The full path to this will will show up as `path` in the examples below.

Usually you want to convert your bitmap to some kind of matrix before doing any crazy stuff:
```
using (var bmp = (Bitmap)Bitmap.FromFile(path))
using (var data = bmp.Lock())
{
    var M = data.ToMatrix(c => c.Lum());
	M.ToBitmap(v => v.GS()).Dump();
}
```

The example above creates a `Matrix<double>` from a `LockedBitmapData` instance using the `Lum` extension method (defined on `Color` values) to get a grayscale value for each RGB color in the source. Next, we call `ToBitmap` to convert the `Matrix<double>` back to a bitmap again, this time using the `GS` function (defined on `double` values) to get back a `Color` instance.

Note that the example above assumes that you're evaluating **Pixlr** in **LINQPad** otherwise the `Dump` method will probably not be available and you'll most likely have to write the bitmap to disk or show it in a window in order to inspect the results.

### why
The whole reason why this thing exists is because I wanted to do operations on bitmaps that might involve matrices and vectors as well. And as I like to do things interactively I wanted to make the API simple and functionally flavored without making too much compromises on speed.

The desire to use this interactively drove the need for a functional API. And the functional API made it a lot easier to optimize *embarrassingly parallel* workloads so we can get a decent performance without comprimising too much on readability.