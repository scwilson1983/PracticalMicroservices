namespace PracticalMicroservices.Events.Entities
{
    public partial class StreamSummary
    {
        public string StreamName { get; set; }
        public long? MessageCount { get; set; }
        public decimal? Percent { get; set; }
    }
}
