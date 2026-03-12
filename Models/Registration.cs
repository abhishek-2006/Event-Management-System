using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Roll Number is required")]
        public string RollNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Semester { get; set; }

        public string Department { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }
}