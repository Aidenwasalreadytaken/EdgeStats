using WebStatistics;
using Xunit;

namespace UnitTests
{
    public class SlidingWindowStatisticsTests
    {
        private const int WINDOW_SIZE = 5;
        private readonly SlidingWindowStatistics _slidingWindowStatistics;
        private readonly DecrementalStandardDeviation _decrementalStatistic;

        public SlidingWindowStatisticsTests()
        {
            _decrementalStatistic = new DecrementalStandardDeviation();
            _slidingWindowStatistics = new SlidingWindowStatistics(_decrementalStatistic, WINDOW_SIZE);
        }

        [Fact]
        public void WhenAddUptoWindow_ShouldNotRemove()
        {
            PopulateStats(WINDOW_SIZE);

            Assert.Equal(5, _decrementalStatistic.Count);
            Assert.Equal(3, _decrementalStatistic.Mean);
            Assert.Equal(2, _decrementalStatistic.Variance);
            TestUtils.AssertEqualityAndSignificance(1.4142135623731, _decrementalStatistic.StandardDeviation);
        }

        [Fact]
        public void WhenAddPastWindow_ShouldRemove()
        {
            PopulateStats(WINDOW_SIZE);
            _slidingWindowStatistics.AddSample(7);

            Assert.Equal(5, _decrementalStatistic.Count);
            TestUtils.AssertEqualityAndSignificance(4.2, _decrementalStatistic.Mean);
            TestUtils.AssertEqualityAndSignificance(2.96, _decrementalStatistic.Variance);
            TestUtils.AssertEqualityAndSignificance(1.7204650534085, _decrementalStatistic.StandardDeviation);
        }

        private void PopulateStats(int count)
        {
            for (int ii = 0; ii < count; ii++)
            {
                _slidingWindowStatistics.AddSample(ii + 1);
            }
        }
    }
}
