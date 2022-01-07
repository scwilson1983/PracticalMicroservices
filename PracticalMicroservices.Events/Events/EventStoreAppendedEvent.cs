namespace PracticalMicroservices.Events.Events
{
    public class EventStoreAppendedEvent
    {
        public long GlobalPostition { get; set; }
    }
}
