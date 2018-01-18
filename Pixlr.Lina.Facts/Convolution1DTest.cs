namespace Pixlr.Lina
{
    using Xunit;

    public class Convolution1DTest
    {
        private readonly Vector<int> source;
        private readonly Vector<int> kernel;
        private readonly IConvolution1D<int> convolution;

        public Convolution1DTest()
        {
            this.source = Vector.Build<int>().Dense(1, 2, 3, 4, 5, 6, 7, 8);
            this.kernel = Vector.Build<int>().Dense(0, 1, 0);
            this.convolution = this.kernel.Convolution((s, u, v) => s + (u * v), i => 0);
        }

        [Fact]
        public void ValidIsSmallerThanSource()
        {
            var w = this.convolution.Valid(this.source);
            Assert.Equal(this.source.Length - 2 * (this.kernel.Length / 2), w.Length);
        }

        [Fact]
        public void SameHasEqualLengthToSource()
        {
            var w = this.convolution.Same(this.source);
            Assert.Equal(this.source.Length, w.Length);
        }

        [Fact]
        public void AllHasLargerLengthThanSource()
        {
            var w = this.convolution.All(this.source);
            Assert.Equal(this.source.Length + 2 * (this.kernel.Length / 2), w.Length);
        }
    }
}