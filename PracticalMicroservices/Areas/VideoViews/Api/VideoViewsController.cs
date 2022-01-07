using Microsoft.AspNetCore.Mvc;
using PracticalMicroservices.Events.Services;
using System;
using PracticalMicroservices.Areas.VideoViews.ViewModels;
using PracticalMicroservices.MaterializedViews.Videos;

namespace PracticalMicroservices.Areas.VideoRecording.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoViewsController : ControllerBase
    {
        readonly IMessageStore _messageStore;
        readonly IVideoService _videoService;
        public static VideoView VideoViewsVm = new VideoView { Id = Guid.NewGuid(), Name = "Test Video", ViewCount = 0 };

        public VideoViewsController(IMessageStore messageStore, IVideoService videoService)
        {
            _messageStore = messageStore;
            _videoService = videoService;
        }

        [HttpGet]
        public ActionResult Get() 
        {
            VideoViewsVm.ViewCount = _videoService.GetCountForHomePage();
            return Ok(VideoViewsVm);
        }

        [HttpPost]
        public IActionResult Create([FromBody] VideoView vm)
        {
            VideoViewsVm.ViewCount++;
            var version = _messageStore.WriteMessage(VideoViewsVm, VideoViewsVm.Id);
            _videoService.IncrementVideosViewed(version);
            return Ok();
        }
    }
}
