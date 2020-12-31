using System;

namespace WebStatistics
{
    public class IncrementalStatistics : BaseStatistics
    {
        public IncrementalStatistics()
            : base()
        {}

        public IncrementalStatistics(int count, double mean, double variance)
            : base (count, mean, variance)
        {}

        public virtual void AddSample(double value)
        {
            double previousMean = _mean;
            _count++;
            IncrementMean(value);
            IncrementVariance(previousMean);
        }

        protected virtual double CalculateMeanOffset(double value)
        {
            return (value - _mean) / _count;
        }

        protected virtual void IncrementMean(double value)
        {
            _mean += CalculateMeanOffset(value);
        }

        protected virtual void IncrementVariance(double previousMean)
        {
            double meanDiff = _mean - previousMean;
            _variance = (1.0 - 1.0 / _count) * _variance;
            _variance += (_count - 1) * (meanDiff * meanDiff);
        }

        public virtual IncrementalStatistics Copy()
        {
            return new IncrementalStatistics(_count, _mean, _variance);
        }
    }
}
