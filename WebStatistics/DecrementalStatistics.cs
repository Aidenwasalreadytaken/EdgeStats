using System;
using System.Collections.Generic;
using System.Text;

namespace WebStatistics
{
    public class DecrementalStatistics : IncrementalStatistics
    {
        public DecrementalStatistics()
               : base()
        { }

        public DecrementalStatistics(int count, double mean, double variance)
            : base(count, mean, variance)
        { }

        public virtual void RemoveSample(double value)
        {
            if (_count > 0)
            {
                double previousMean = _mean;
                _count--;
                DecrementMean(value);
                DecrementVariance(previousMean);
            }
            else
            {
                throw new InvalidOperationException("Cannot remove point when count is zero");
            }
        }

        protected override double CalculateMeanOffset(double value)
        {
            // If count if now 0, offset should make mean 0
            double offset = _mean;

            if (_count > 0)
            {
                offset = base.CalculateMeanOffset(value);
            }

            return offset;
        }

        protected virtual void DecrementMean(double value)
        {
            _mean -= CalculateMeanOffset(value);
        }

        //variance = Sn = n * (variance of n elements)
        protected virtual void DecrementVariance(double previousMean)
        {
            if (_count > 0)
            {
                double meanDiff = _mean - previousMean;
                _variance -= _count * (meanDiff * meanDiff);
                _variance += 1.0 / _count * _variance;
            }
            else
            {
                _variance = 0.0;
            }
        }

        public virtual DecrementalStatistics Copy()
        {
            return new DecrementalStatistics(_count, _mean, _variance);
        }
    }
}
