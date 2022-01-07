using System.ComponentModel.DataAnnotations;

namespace PracticalMicroservices.MaterializedViews.Entities
{
    public class Page : ViewEntity
    {
        [Key]
        public string Name { get; set; }
        public string Data { get; set; } = "{}";
    }
}
