using System;

namespace WebStatistics
{
    public class BaseStandardDeviation
    {
        internal readonly BaseMean _baseMean;
        internal double _variance;

        public BaseStandardDeviation(BaseMean baseMean)
        {
            if (baseMean == null)
            {
                throw new ArgumentNullException();
            }
            _baseMean = baseMean;
            _variance = 0.0;
        }

        public BaseStandardDeviation(BaseMean baseMean, double variance)
        {
            if (baseMean == null)
            {
                throw new ArgumentNullException();
            }
            _baseMean = baseMean;
            _variance = variance;
        }

        public virtual int Count { get => _baseMean._count; }

        public virtual double Mean { get => _baseMean._mean; }

        public virtual double Variance { get => _variance; }

        public virtual double StandardDeviation
        {
            get => Math.Sqrt(_variance);
        }

        public virtual BaseStandardDeviation Copy()
        {
            return new BaseStandardDeviation(_baseMean.Copy(), _variance);
        }
    }
}
