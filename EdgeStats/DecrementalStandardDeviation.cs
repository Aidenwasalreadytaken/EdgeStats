using System;

namespace EdgeStats
{
    public class DecrementalStandardDeviation : IncrementalStandardDeviation, IDecrementalStatistic
    {
        public DecrementalStandardDeviation()
               : base(new DecrementalMean())
        { }

        public DecrementalStandardDeviation(DecrementalMean decrementalMean)
            : base (decrementalMean)
        { }

        public DecrementalStandardDeviation(DecrementalMean decrementalMean, double variance)
            : base(decrementalMean, variance)
        { }

        public virtual void RemoveSample(double value)
        {
            if (_baseMean._count > 0)
            {
                double previousMean = _baseMean._mean;
                ((DecrementalMean)_baseMean).RemoveSample(value);
                DecrementVariance(previousMean);
            }
            else
            {
                throw new InvalidOperationException("Cannot remove point when count is zero");
            }
        }

        //variance = Sn = n * (variance of n elements)
        protected virtual void DecrementVariance(double previousMean)
        {
            if (_baseMean._count > 0)
            {
                double meanDiff = _baseMean._mean - previousMean;
                _variance -= _baseMean._count * (meanDiff * meanDiff);
                _variance += 1.0 / _baseMean._count * _variance;
            }
            else
            {
                _variance = 0.0;
            }
        }

        public virtual DecrementalStandardDeviation Copy()
        {
            return new DecrementalStandardDeviation((DecrementalMean)_baseMean, _variance);
        }
    }
}
