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
        public string Title { get; set; } = string.Empty;
        public string? Slug { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string Category { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [Required]
        [Display(Name = "Registration Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationStartDate { get; set; }

        [Required]
        [Display(Name = "Registration End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationEndDate { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;
        public bool IsRegistrationOpen => DateTime.Now.Date >= RegistrationStartDate.Date && DateTime.Now.Date <= RegistrationEndDate.Date;
        public int DaysUntilDeadline => (RegistrationEndDate.Date - DateTime.Now.Date).Days;

        [NotMapped]
        public int TotalRegistrations { get; set; }
    }
}