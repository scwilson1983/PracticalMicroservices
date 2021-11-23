using System;

namespace PracticalMicroservices.Events.Entities
{
    public partial class Message
    {
        public long GlobalPosition { get; set; }
        public long Position { get; set; }
        public DateTime Time { get; set; }
        public string StreamName { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public string Metadata { get; set; }
        public Guid Id { get; set; }
    }
}
