using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data;
using EventManagementSystem.Models;

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
            // Set the registration time manually to ensure it's accurate
            registration.RegisteredAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction("RegistrationSuccess");
            }

            // If we return to the view because of an error, we need to set the title again
            var ev = _context.Events.Find(registration.EventId);
            ViewBag.EventTitle = ev?.Title;
            
            return View(registration);
        }

        public IActionResult RegistrationSuccess()
        {
            return View();
        }

        // Show create form
        public IActionResult Create()
        {
            return View();
        }

        // Save event
        [HttpPost]
        public IActionResult Create(Event ev)
        {
            if (ModelState.IsValid)
            {
                ev.Slug = ev.Title.ToLower().Replace(" ", "-");
                _context.Events.Add(ev);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ev);
        }
    }
}