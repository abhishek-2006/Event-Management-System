namespace EventManagementSystem.ViewModels
{
    public class RegistrationViewModel
    {
        public int RegistrationId { get; set; }

        public string EventTitle { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Department { get; set; }

        public int Semester { get; set; }

        public string Email { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}