using PracticalMicroservices.MaterializedViews.Videos;

namespace PracticalMicroservices.Domain.Home
{
    public class HomeAggregator : IHomeAggregator
    {
        readonly IVideoService _videoService;

        public HomeAggregator(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public void IncrementVideosViewed(long globalPosition)
        {
            _videoService.IncrementVideosViewed(globalPosition);
        }
    }
}
