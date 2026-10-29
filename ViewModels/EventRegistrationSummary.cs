namespace EventManagementSystem.ViewModels
{
    public class EventRegistrationSummary
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Slug { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public int RegistrationsCount { get; set; }
    }
}
