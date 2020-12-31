using System;
using System.Collections.Generic;
using System.Text;

namespace WebStatistics
{
    public class SlidingWindowStatistics
    {
        private int _windowSize;
        private LinkedList<double> _window;
        private DecrementalStatistics _statistics;

        public SlidingWindowStatistics(int windowSize)
                  : base()
        {
            _windowSize = windowSize;
            _window = new LinkedList<double>();
            _statistics = new DecrementalStatistics();
        }

        public virtual void AddSample(double sample)
        {
            if (_window.Count == _windowSize)
            {
                _statistics.RemoveSample(_window.First.Value);
                _window.RemoveFirst();
            }

            _statistics.AddSample(sample);
            _window.AddLast(sample);
        }

        public virtual int Count { get => _statistics.Count; }

        public virtual double Mean { get => _statistics.Mean; }

        public virtual double Variance { get => _statistics.Variance; }

        public virtual double StandardDeviation { get => _statistics.StandardDeviation; }
    }
}
