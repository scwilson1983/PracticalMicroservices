namespace PracticalMicroservices.Domain.Home
{
    public interface IHomeAggregator
    {
        void IncrementVideosViewed(long globalPosition);
    }
}
