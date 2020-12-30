using System.Threading;

namespace LowLevelExercises.Core
{
    /// <summary>
    /// A simple class for reporting a specific value and obtaining an average.
    /// </summary>
    /// TODO: remove the locking and use <see cref="Interlocked"/> and <see cref="Volatile"/> to implement a lock-free implementation.
    public class AverageMetric
    {
        int sum = 0;
        int count = 0;
        double cachedAverage = 0d;

        public void Report(int value)
        {
            // TODO: how to increment sum + count without locking?
            Interlocked.Add(ref sum, value);
            Interlocked.Increment(ref count);
            Interlocked.Exchange(ref cachedAverage, 0d);
        }

        public double Average
        {
            get
            {
                // TODO: how to access the values in a lock-free way?
                // let's assume that we can return value estimated on a bit stale data(in time average will be less and less diverged)

                if (cachedAverage == 0)
                {
                    Interlocked.CompareExchange(ref cachedAverage, Calculate(count, sum), 0d);
                }

                return cachedAverage;
            }
        }

        static double Calculate(in int count, in int sum)
        {
            // DO NOT change the way calculation is done.

            if (count == 0)
            {
                return double.NaN;
            }

            return (double)sum / count;
        }
    }
}
