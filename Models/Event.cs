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

        [Required]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Required]
        [Display(Name = "Registration Start Date")]
        public DateTime RegistrationStartDate { get; set; }

        [Required]
        [Display(Name = "Registration End Date")]
        public DateTime RegistrationEndDate { get; set; }

        [Required]
        public string Location { get; set; }

        /// <summary>
        /// Returns true only when today is within the registration window.
        /// </summary>
        public bool IsRegistrationOpen =>
            DateTime.Now.Date >= RegistrationStartDate.Date &&
            DateTime.Now.Date <= RegistrationEndDate.Date;

        /// <summary>
        /// Days remaining until the registration window closes.
        /// </summary>
        public int DaysUntilDeadline => (RegistrationEndDate.Date - DateTime.Now.Date).Days;
    }
}