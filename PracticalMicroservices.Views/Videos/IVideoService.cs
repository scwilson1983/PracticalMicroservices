using System;

namespace PracticalMicroservices.MaterializedViews.Videos
{
    public interface IVideoService
    {
        void IncrementVideosViewed(long globalPosition);
        int GetCountForHomePage();
    }
}
