using System;
using System.Collections.Generic;
using System.Text;

namespace WebStatistics
{
    public interface IIncrementalStatistics
    {
        void AddSample(double sample);
    }
}
