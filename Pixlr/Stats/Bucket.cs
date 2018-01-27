namespace Pixlr.Stats
{
    public class Bucket
    {
        public Bucket(double lowerBound, double upperBound)
        {
            this.Count = 0;
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
        }

        public int Count { get; internal set; }

        public double LowerBound { get; internal set; }

        public double UpperBound { get; internal set; }
    }
}