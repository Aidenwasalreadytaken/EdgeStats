
namespace EdgeStats
{
    public interface IDecrementalStatistic : IIncrementalStatistics
    {
        void RemoveSample(double sample);
    }
}
