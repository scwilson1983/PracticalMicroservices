using PracticalMicroservices.Events.Entities;

namespace PracticalMicroservices.MaterializedViews.Videos
{
    public interface IVideoService
    {
        void IncrementVideosViewed(Message message);
        int GetCountForHomePage();
    }
}
