namespace Pixlr.Lina.Facts
{
    using System;
    using Xunit;

    public class VectorTest
    {
        [Fact]
        public void LengthShouldEqualNumberOfElements()
        {
            var v = Vector.Build<int>().Dense(0, 1, 2);
            Assert.Equal(3, v.Length);
        }

        [Fact]
        public void FirstElementStartsAtIndexZero()
        {
            var v = Vector.Build<int>().Dense(0, 1, 2);
            Assert.Equal(0, v[0]);
            Assert.Equal(1, v.At(1));
        }

        [Fact]
        public void MutateElementsInPlace()
        {
            var v = Vector.Build<int>().Dense(0, 1, 2);
            v[0] = 123;
            Assert.Equal(123, v.At(0));
        }

        [Fact]
        public void CalculateSumForIntVectors()
        {
            var v = Vector.Build<int>().Dense(1, 2, 3, 4, 5);
            Assert.Equal(15, v.Sum());
        }

        [Fact]
        public void CalculateSumForDoubleVectors()
        {
            var v = Vector.Build<double>().Dense(1, 2, 3, 4, 5);
            Assert.Equal(15, v.Sum());
        }

        [Fact]
        public void CalculateCentroidForIntVectors()
        {
            var cases = new[]
            {
                new { V = Vector.Build<int>().Dense(1, 0, 0, 0, 1), Exp = 2.0 },
                new { V = Vector.Build<int>().Dense(1, 1, 0, 0, 0), Exp = 0.5 },
                new { V = Vector.Build<int>().Dense(0, 0, 0, 1, 1), Exp = 3.5 },
            };

            Array.ForEach(cases, c => Assert.Equal(c.Exp, c.V.Centroid()));
        }
    }
}
