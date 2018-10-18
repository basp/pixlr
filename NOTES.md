## introduction
* Affine combination
* Barycentric coordinates

### affine combination
An **affine combination** is a linear combination of vectors `x[1]..x[n]`:
```
a*x = a[1]x[1] + a[2]x[2] + ... + a[n]x[n]
```
Where the sum of the coefficients `a[1..n]` is `1`.

Take for example two points `P1` and `P2` and the **affine combination**:
```
P(t) = (1 - t)P1 + tP2
```

The above combination is what is commonly known as **linear interpolation**. Often named `lerp` in source code. It will plot a straight line between two points.

I assume you can plot *exponential* functions using an **affine combination** with two components.

If you have the three polynomials:
```
U = (1 - t)^2
V = 2t(1 - t)
W = t^2
```

And you sum them then `U + V + W = 1` which means they are an **affine combination** as well. 

With 3 points `P1`, `P2` and `P3` you can get to any point on a curve between these points using: 
```
P(t) = (1 - t)^2P1 + 2t(1 - t)P2 + t^2P3
```

The polynomials (the *alpha* values in this case) are called **Bernstein polynomials**. They can be used to make curves. The curve above is a **parabola**.

If you have the following **affine combination**:
```
(1 - t)^3
3t(1 - t)^2
3t^2(1 - t)
t^3
```

In combination with four points `P1`, `P2`, `P3` and `P4` you can draw a curve between those points as well using:
```
P(t) = (1 - t)^3P1 + 3t(1 - t)^2P2 + 3t^2(1 - t)P3 + t^3P4
```

The curve above is a **cubic** curve.

You can rewrite the curve definition above in terms of vectors and matrices:
```
                   [  1  0  0  0 ]   [ P1 ]
[ 1, t, t2, t3 ] * [ -3  3  0  0 ] * [ P2 ]
                   [  3 -6  3  0 ]   [ P3 ]
                   [ -1  3 -3  1 ]   [ P4 ] 
```

The fact that we an rewrite our polynomials like this also means we can do the necessary calculations relatively fast.

## curves in the plane
* Chaikin's algorithm
* Corner cutting algorithm
* Bezier curves
* Catmull-Clark subdivision surface

Control points are also known as Chaikin's points or Bezier points. A straight up interpolating polynomial doesn't work because it creates weird curves where straight lines should be.

### chaikin's algorithm
1. Take a set of points `P0 = {P0[1], .. P0[n]}` where `|P0| >= 3`.
2. Create a set of vectors `V` between those points where `V[i] = P[i + 1] - P[i]` and `|V| = |P0| - 1`.
3. Create a new set of points `P1` by creating two new points for each vector in `V` at `1/3*V` and `2/3*V` so that `P1[i] = 1/3*V` and `P1[i + 1] = 2/3V` where `|P1| = |V| * 2`.
4. Set `P0 = P1` and repeat as desired.

Chaikin can be done in 3D but there's some problems so everybody uses **Catmull-Clark**.

### bezier's curve



