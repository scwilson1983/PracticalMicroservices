using Microsoft.AspNetCore.Mvc;
using PracticalMicroservices.Events.Services;
using System;
using PracticalMicroservices.Areas.VideoViews.ViewModels;

namespace PracticalMicroservices.Areas.VideoRecording.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoViewsController : ControllerBase
    {
        readonly IMessageStore _messageStore;
        public static VideoView VideoViewsVm = new VideoView { Id = Guid.NewGuid(), Name = "Test Video", ViewCount = 1 };

        public VideoViewsController(IMessageStore messageStore)
        {
            _messageStore = messageStore;
        }

        [HttpGet]
        public ActionResult Get() => Ok(VideoViewsVm);

        [HttpPost]
        public IActionResult Create([FromBody] VideoView vm)
        {
            VideoViewsVm.ViewCount++;
            var version = _messageStore.WriteMessage(VideoViewsVm, VideoViewsVm.Id);
            return Ok();
        }
    }
}
