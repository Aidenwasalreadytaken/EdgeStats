using System;
using System.Collections.Generic;
using System.Text;

namespace WebStatistics
{
    public class SlidingWindowStatistics
    {
        private int _windowSize;
        private LinkedList<double> _window;
        private IDecrementalStatistic _decrementalStatistic;

        public SlidingWindowStatistics(IDecrementalStatistic decrementalStatistic, int windowSize)
                  : base()
        {
            _windowSize = windowSize;
            _window = new LinkedList<double>();
            _decrementalStatistic = decrementalStatistic;
        }

        public virtual void AddSample(double sample)
        {
            if (_window.Count == _windowSize)
            {
                _decrementalStatistic.RemoveSample(_window.First.Value);
                _window.RemoveFirst();
            }

            _decrementalStatistic.AddSample(sample);
            _window.AddLast(sample);
        }
    }
}
