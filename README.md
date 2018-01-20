## pixlr
Functional operations and combinators for bitmaps.

### usage
Convert a `Color` instance to its *relative luminosity* value:
```
var color = Color.FromArgb(100, 50, 200);
var lum = color.Lum();
```

Convert a `double` to a grayscale color:
```
var v = Color.FromArgb(100, 50, 200).Lum();
var color = v.GS();
```

Get a `LockedBitmapData` instance:
```
var bmp = (Bitmap)Bitmap.FromFile(@"path/to/bmp")
using(var data = bmp.Lock())
{
    // ...
}
```

Convert a `LockedBitmapData` to a `Matrix<U>` instance:
```
using(var data = bmp.Lock())
{
    var m = data.ToMatrix(color => color.Lum());
    // ...
}
```

Convert a `Matrix<U>` back into a `Bitmap` instance:
```
using(var data = bmp.Lock())
{
    var m = data.ToMatrix(color => color.Lum());
    var bmp = m.ToBitmap(v => v.GS());
    // ...
}
```