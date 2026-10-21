namespace EventManagementSystem.ViewModels
{
    public class RegistrationViewModel
    {
        public int RegistrationId { get; set; }

        public string EventTitle { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public long EnrollNumber { get; set; }

        public string Department { get; set; } = string.Empty;

        public int Semester { get; set; }

        public string Email { get; set; } = string.Empty;

        public DateTime RegisteredAt { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime RegistrationStartDate { get; set; }

        public DateTime RegistrationEndDate { get; set; }
    }
}