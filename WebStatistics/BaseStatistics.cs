using System;

namespace WebStatistics
{
    public class BaseStatistics
    {
        protected int _count;
        protected double _mean;
        protected double _variance;

        public BaseStatistics()
        {
            _count = 0;
            _mean = 0.0;
            _variance = 0.0;
        }

        public BaseStatistics(int count, double mean, double variance)
        {
            _count = count;
            _mean = mean;
            _variance = variance;
        }

        public virtual int Count {  get => _count; }

        public virtual double Mean { get => _mean; }

        public virtual double Variance { get => _variance; }

        public virtual double StandardDeviation
        {
            get => Math.Sqrt(_variance);
        }

        public virtual BaseStatistics Copy()
        {
            return new BaseStatistics(_count, _mean, _variance);
        }
    }
}
