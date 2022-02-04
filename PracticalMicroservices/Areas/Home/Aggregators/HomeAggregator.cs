using PracticalMicroservices.MaterializedViews.Videos;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System;
using PracticalMicroservices.Events.Services;

namespace PracticalMicroservices.Home.Aggregators
{
    public class HomeAggregator : IHostedService, IDisposable
    {
        readonly IVideoService _videoService;
        readonly IMessageStore _messageStore;
        Timer _timer;

        public HomeAggregator(IVideoService videoService, IMessageStore messageStore)
        {
            _videoService = videoService;
            _messageStore = messageStore;
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
            var nextMessages = _messageStore.ReadMessages(new ReadMessageRequest());
            foreach(var next in nextMessages)
            {
                _videoService.IncrementVideosViewed(next);
            }
        }
    }
}
