namespace PracticalMicroservices.Events.Entities
{
    public partial class CategoryTypeSummary
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public long? MessageCount { get; set; }
        public decimal? Percent { get; set; }
    }
}
