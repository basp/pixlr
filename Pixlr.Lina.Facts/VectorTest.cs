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
    }
}
