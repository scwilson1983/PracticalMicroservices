using System;

namespace PracticalMicroservices.Areas.VideoViews.ViewModels
{
    public class VideoView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ViewCount { get; set; } = 0;
    }
}
