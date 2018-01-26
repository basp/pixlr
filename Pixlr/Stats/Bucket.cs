namespace Pixlr.Stats
{
    public class Bucket
    {
        public Bucket(int count, double lowerBound, double upperBound)
        {
            this.Count = count;
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
        }

        public int Count { get; private set; }

        public double LowerBound { get; private set; }

        public double UpperBound { get; private set; }
    }
}