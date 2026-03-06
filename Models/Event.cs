    using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public string Location { get; set; }
    }
}