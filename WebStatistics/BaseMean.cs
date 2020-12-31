using System;
using System.Collections.Generic;
using System.Text;

namespace WebStatistics
{
    public class BaseMean
    {
        internal int _count;
        internal double _mean;

        public BaseMean()
        {
            _count = 0;
            _mean = 0.0;
        }

        public BaseMean(int count, double mean)
        {
            _count = count;
            _mean = mean;
        }

        public virtual int Count { get => _count; }

        public virtual double Mean { get => _mean; }

        public virtual BaseMean Copy()
        {
            return new BaseMean(_count, _mean);
        }
    }
}
