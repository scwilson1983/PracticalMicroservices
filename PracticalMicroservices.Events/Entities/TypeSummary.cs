namespace PracticalMicroservices.Events.Entities
{
    public partial class TypeSummary
    {
        public string Type { get; set; }
        public long? MessageCount { get; set; }
        public decimal? Percent { get; set; }
    }
}
