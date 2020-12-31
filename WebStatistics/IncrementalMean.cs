using System;
using System.Collections.Generic;
using System.Text;

namespace WebStatistics
{
    public class IncrementalMean : BaseMean, IIncrementalStatistics
    {
        public IncrementalMean()
            : base()
        { }

        public IncrementalMean(int count, double mean)
            : base(count, mean)
        { }

        public virtual void AddSample(double value)
        {
            _count++;
            _mean += (value - _mean) / _count;
        }

        public virtual IncrementalMean Copy()
        {
            return new IncrementalMean(_count, _mean);
        }
    }
}
