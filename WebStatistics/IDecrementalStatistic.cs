using System;
using System.Collections.Generic;
using System.Text;

namespace WebStatistics
{
    public interface IDecrementalStatistic : IIncrementalStatistics
    {
        void RemoveSample(double sample);
    }
}
