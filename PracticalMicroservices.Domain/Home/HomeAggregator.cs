using PracticalMicroservices.MaterializedViews.Videos;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace PracticalMicroservices.Domain.Home
{
    public class HomeAggregator : IHostedService, IDisposable
    {
        readonly IVideoService _videoService;
        Timer _timer;

        public HomeAggregator(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public void IncrementVideosViewed(long globalPosition)
        {
            _videoService.IncrementVideosViewed(globalPosition);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ProcessOutstandingEvents, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(200));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask; 
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        void ProcessOutstandingEvents(object? state)
        {

        }
    }
}
