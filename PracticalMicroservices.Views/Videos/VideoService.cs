using PracticalMicroservices.Events.Entities;
using PracticalMicroservices.MaterializedViews.Entities;
using PracticalMicroservices.MaterializedViews.Infrastructure;
using System.Linq;
using System.Text.Json;

namespace PracticalMicroservices.MaterializedViews.Videos
{
    public class VideoService : IVideoService
    {
        readonly MaterializedViewsContext _viewsContext;

        public VideoService(MaterializedViewsContext viewsContext)
        {
            _viewsContext = viewsContext;
        }

        public int GetCountForHomePage()
        {
            var homePage = _viewsContext.Pages.FirstOrDefault(p => p.Name == "Home");
            if(homePage == null)
            {
                return 0;
            }
            var data = JsonSerializer.Deserialize<VideoPageData>(homePage.Data);
            return data.VideosWatched;
        }

        public void IncrementVideosViewed(Message message)
        {
            var globalPosition = message.GlobalPosition;
            var homePage = _viewsContext.Pages.FirstOrDefault(p => p.Name == "Home");
            if(homePage == null)
            {
                var pageData = new VideoPageData
                {
                    VideosWatched = 1,
                    LastViewProcessed = globalPosition
                };
                homePage = new Page
                {
                    GlobalPosition = globalPosition,
                    Name = "Home",
                    Data = JsonSerializer.Serialize(pageData) 
                };
                _viewsContext.Add(homePage);
                _viewsContext.SaveChanges();
            }
            else
            {
                var pageData = JsonSerializer.Deserialize<VideoPageData>(homePage.Data);
                if(pageData.LastViewProcessed < globalPosition)
                {
                    pageData.VideosWatched++;
                    homePage.Data = JsonSerializer.Serialize(pageData);
                    homePage.GlobalPosition = globalPosition;
                    _viewsContext.SaveChanges();
                }
            }
        }
    }
}
