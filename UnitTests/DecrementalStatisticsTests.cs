using System;
using System.Diagnostics;
using WebStatistics;
using Xunit;

namespace UnitTests
{
    public class DecrementalStatisticsTests
    {
        private readonly DecrementalStandardDeviation _statistics;

        public DecrementalStatisticsTests()
        {
            _statistics = new DecrementalStandardDeviation();

            _statistics.AddSample(1.5);
            _statistics.AddSample(5.2);
            _statistics.AddSample(658.5);
            _statistics.AddSample(785658.5);
        }

        [Fact]
        public void WhenAdd_CountShouldBeCorrect()
        {
            double expected = 4;
            double actual = _statistics.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenAdd_MeanShouldBeCorrect()
        {
            double expected = 196580.925;
            double actual = _statistics.Mean;

            TestUtils.AssertEqualityAndSignificance(expected, actual);
        }

        [Fact]
        public void WhenAdd_VarianceShouldBeCorrect()
        {
            double expected = 115670867994.59;
            double actual = _statistics.Variance;

            TestUtils.AssertEqualityAndSignificance(expected, actual);
        }

        [Fact]
        public void WhenAdd_StdShouldBeCorrect()
        {
            double expected = 340104.20167148;
            double actual = _statistics.StandardDeviation;

            TestUtils.AssertEqualityAndSignificance(expected, actual);
        }

        [Fact]
        public void WhenRemove_CountShouldEqual()
        {
            double expected = 3;

            _statistics.RemoveSample(785658.5);

            double actual = _statistics.Count;


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenRemove_MeanShouldEqual()
        {
            double expected = 221.733333333;

            _statistics.RemoveSample(785658.5);

            double actual = _statistics.Mean;


            TestUtils.AssertEqualityAndSignificance(expected, actual);
        }

        [Fact]
        public void WhenRemove_VarianceShouldEqual()
        {
            double expected = 95384.8422;

            _statistics.RemoveSample(785658.5);

            double actual = _statistics.Variance;


            TestUtils.AssertEqualityAndSignificance(expected, actual);
        }

        [Fact]
        public void WhenRemove_StdShouldEqual()
        {
            double expected = 308.844;

            _statistics.RemoveSample(785658.5);

            double actual = _statistics.StandardDeviation;


            TestUtils.AssertEqualityAndSignificance(expected, actual);
        }

        [Fact]
        public void WhenRemoveAll_StatisticsShouldBeZero()
        {
            _statistics.RemoveSample(1.5);
            _statistics.RemoveSample(5.2);
            _statistics.RemoveSample(658.5);
            _statistics.RemoveSample(785658.5);

            Assert.Equal(0, _statistics.Count);
            Assert.Equal(0.0, _statistics.Mean);
            Assert.Equal(0.0, _statistics.Variance);
            Assert.Equal(0.0, _statistics.StandardDeviation);
        }

        [Fact]
        public void WhenCountZeroAndRemove_ShouldThrow()
        {
            DecrementalStandardDeviation testStatistics = new DecrementalStandardDeviation();

            Assert.Throws<InvalidOperationException>(() => testStatistics.RemoveSample(Double.MaxValue));
        }
    }
}
