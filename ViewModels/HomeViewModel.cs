using EventManagementSystem.Models;

namespace EventManagementSystem.ViewModels
{
    public class HomeViewModel
    {
        public List<Event> LiveEvents { get; set; } = new();
        public List<Event> PastEvents { get; set; } = new();
    }
}
