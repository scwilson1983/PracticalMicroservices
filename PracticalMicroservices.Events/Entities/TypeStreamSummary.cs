﻿namespace PracticalMicroservices.Events.Entities
{
    public partial class TypeStreamSummary
    {
        public string Type { get; set; }
        public string StreamName { get; set; }
        public long? MessageCount { get; set; }
        public decimal? Percent { get; set; }
    }
}
