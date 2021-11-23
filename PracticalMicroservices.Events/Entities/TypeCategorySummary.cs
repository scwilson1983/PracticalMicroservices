namespace PracticalMicroservices.Events.Entities
{
    public partial class TypeCategorySummary
    {
        public string Type { get; set; }
        public string Category { get; set; }
        public long? MessageCount { get; set; }
        public decimal? Percent { get; set; }
    }
}
