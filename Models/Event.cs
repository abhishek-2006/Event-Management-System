using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string? Slug { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Category { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime EventDate { get; set; }

        [Required]
        public string Location { get; set; }
    }
}