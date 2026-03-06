using System;

namespace EventManagementSystem.Models
{
    public class Registration
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public User User { get; set; }

        public Event Event { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}