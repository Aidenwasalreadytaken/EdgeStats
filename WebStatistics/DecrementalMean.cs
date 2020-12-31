using System;
using System.Collections.Generic;
using System.Text;

namespace WebStatistics
{
    public class DecrementalMean : IncrementalMean, IDecrementalStatistic
    {
        public DecrementalMean()
               : base()
        { }

        public DecrementalMean(int count, double mean)
            : base(count, mean)
        { }

        public virtual void RemoveSample(double value)
        {
            if (_count > 0)
            {
                _count--;

                if (_count > 0)
                {
                    _mean -= (value - _mean) / _count;
                }
                else
                {
                    _mean -= _mean;
                }
            }
            else
            {
                throw new InvalidOperationException("Cannot remove point when count is zero");
            }
        }

        public virtual DecrementalMean Copy()
        {
            return new DecrementalMean(_count, _mean);
        }
    }
}
