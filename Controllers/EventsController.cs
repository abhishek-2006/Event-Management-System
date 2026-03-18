using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;

namespace EventManagementSystem.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // List all events
        public IActionResult Index()
        {
            var events = _context.Events.ToList();
            return View(events);
        }

        [HttpGet("events/register/{slug}")]
        public IActionResult Register(string slug)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Slug == slug);
            if (ev == null) return NotFound();

            ViewBag.EventTitle = ev.Title;

            return View(new Registration { EventId = ev.EventId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Registration registration)
        {
            ModelState.Remove("Event");
            
            registration.RegisteredAt = DateTime.Now;

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("MODEL STATE IS VALID");
                var isAlreadyRegistered = _context.Registrations.Any(r => 
                    r.RollNumber == registration.RollNumber && 
                    r.EventId == registration.EventId);
                if (isAlreadyRegistered)                {
                    ModelState.AddModelError("RollNumber", "You have already registered for this event.");
                    Console.WriteLine("ROLL NUMBER ALREADY EXISTS");
                    return View("RegistrationSuccess");
                }
                else
                {
                    _context.Registrations.Add(registration);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction("RegistrationSuccess", new { id = registration.Id });
                }
            }

            var ev = await _context.Events.FindAsync(registration.EventId);
            if (ev != null)
            {
                ViewBag.EventTitle = ev.Title;
                ViewData["EventSlug"] = ev.Slug;
                Console.WriteLine($"EVENT FOUND: {ev.Title}");
                Console.WriteLine($"EVENT SLUG: {ev.Slug}");
                Console.WriteLine($"EVENT ID: {ev.EventId}");
            }
            else
            {
                Console.WriteLine("EVENT NOT FOUND");
            }
            
            return View(registration);
        }

        [HttpGet("events/RegistrationSuccess")]
        public IActionResult RegistrationSuccess(int id)
        {
            // Fetch the registration and include the Event details
            var registration = _context.Registrations
                .Where(r => r.Id == id)
                .Select(r => new RegistrationViewModel
                {
                    RegistrationId = r.Id,
                    Name = r.StudentName,
                    RollNumber = r.RollNumber,
                    EventTitle = r.Event.Title,
                    Semester = r.Semester,
                    Department = r.Department,
                    RegisteredAt = r.RegisteredAt
                })
                .FirstOrDefault();

            if (registration == null) return RedirectToAction("Index");

            return View(registration);
        }
        
        [HttpGet("events/{slug}")]
        public IActionResult Details(string slug)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Slug == slug);
            if (ev == null) return NotFound();

            ViewBag.EventTitle = ev.Title;

            return View(ev);
        }
    }
}