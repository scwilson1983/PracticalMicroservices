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

        public void IncrementVideosViewed(long globalPosition)
        {
            var homePage = _viewsContext.Pages.FirstOrDefault(p => p.Name == "Home");
            if(homePage == null)
            {
                var pageData = new VideoPageData
                {
                    VideosWatched = 1
                };
                homePage = new Page
                {
                    GlobalPosition = globalPosition,
                    Name = "Home",
                    Data = JsonSerializer.Serialize(pageData) 
                };
                _viewsContext.Add(homePage);
            }
            else
            {
                var pageData = JsonSerializer.Deserialize<VideoPageData>(homePage.Data);
                pageData.VideosWatched++;
                homePage.Data = JsonSerializer.Serialize(pageData);
                homePage.GlobalPosition = globalPosition;
            }
            _viewsContext.SaveChanges();
        }
    }
}
