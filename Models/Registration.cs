using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventManagementSystem.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enrollment Number is required")]
        [Display(Name = "Enrollment Number")]
        public long EnrollNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Semester is required")]
        public int Semester { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; } = string.Empty;

        public int EventId { get; set; }

        [ValidateNever]
        public Event? Event { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }
}