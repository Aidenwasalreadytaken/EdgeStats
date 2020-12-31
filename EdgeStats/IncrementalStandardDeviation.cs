
namespace EdgeStats
{
    public class IncrementalStandardDeviation : BaseStandardDeviation, IIncrementalStatistics
    {
        public IncrementalStandardDeviation()
            : base(new IncrementalMean())
        {}

        public IncrementalStandardDeviation(IncrementalMean incrementalMean)
            : base(incrementalMean)
        { }

        public IncrementalStandardDeviation(IncrementalMean incrementalMean, double variance)
            : base (incrementalMean, variance)
        {}

        public virtual void AddSample(double value)
        {
            double previousMean = _baseMean._mean;
            ((IncrementalMean)_baseMean).AddSample(value);
            IncrementVariance(previousMean);
        }

        protected virtual void IncrementVariance(double previousMean)
        {
            double meanDiff = _baseMean._mean - previousMean;
            _variance = (1.0 - 1.0 / _baseMean._count) * _variance;
            _variance += (_baseMean._count - 1) * (meanDiff * meanDiff);
        }

        public virtual IncrementalStandardDeviation Copy()
        {
            return new IncrementalStandardDeviation((IncrementalMean)_baseMean, _variance);
        }
    }
}
