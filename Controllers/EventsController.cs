using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

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

            var today = DateTime.Now.Date;
            if (today < ev.RegistrationStartDate.Date)
            {
                TempData["Error"] = $"Registration opens on {ev.RegistrationStartDate:MMM dd, yyyy}. You cannot register yet.";
                return RedirectToAction("Details", new { slug = ev.Slug });
            }

            if (today > ev.RegistrationEndDate.Date)
            {
                TempData["Error"] = $"Registration closed on {ev.RegistrationEndDate:MMM dd, yyyy}. Registrations are no longer accepted.";
                return RedirectToAction("Details", new { slug = ev.Slug });
            }

            ViewData["EventTitle"] = ev.Title;
            ViewData["EventSlug"] = ev.Slug;

            return View(new Registration { EventId = ev.EventId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Registration registration)
        {
            ModelState.Remove("Event");

            var ev = await _context.Events.FindAsync(registration.EventId);

            if (ev == null)
            {
                return NotFound();
            }

            var today = DateTime.Now.Date;

            // Validate registration window
            if (today < ev.RegistrationStartDate.Date)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"Registration has not opened yet. It opens on {ev.RegistrationStartDate:MMM dd, yyyy}."
                );
                ViewData["EventSlug"] = ev.Slug;
                ViewData["EventTitle"] = ev.Title;
                return View(registration);
            }

            if (today > ev.RegistrationEndDate.Date)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"Registration deadline has passed ({ev.RegistrationEndDate:MMM dd, yyyy}). Registrations are no longer accepted."
                );
                ViewData["EventSlug"] = ev.Slug;
                ViewData["EventTitle"] = ev.Title;
                return View(registration);
            }

            registration.RegisteredAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                var isAlreadyRegistered = await _context.Registrations.AnyAsync(r =>
                    r.EnrollNumber == registration.EnrollNumber &&
                    r.EventId == registration.EventId);

                if (isAlreadyRegistered)
                {
                    ModelState.AddModelError(
                        "EnrollNumber",
                        "You have already registered for this event."
                    );

                    ViewData["EventSlug"] = ev.Slug;
                    ViewData["EventTitle"] = ev.Title;

                    return View(registration);
                }

                _context.Registrations.Add(registration);
                await _context.SaveChangesAsync();

                return RedirectToAction(
                    "RegistrationSuccess",
                    new { id = registration.Id }
                );
            }

            ViewData["EventTitle"] = ev.Title;
            ViewData["EventSlug"] = ev.Slug;

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
                    EnrollNumber = r.EnrollNumber,
                    EventTitle = r.Event.Title,
                    Semester = r.Semester,
                    Department = r.Department,
                    Email = r.Email,
                    Phone = r.PhoneNumber,
                    RegisteredAt = r.RegisteredAt,
                    EventDate = r.Event.EventDate,
                    RegistrationStartDate = r.Event.RegistrationStartDate,
                    RegistrationEndDate = r.Event.RegistrationEndDate
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

            ViewData["EventSlug"] = ev.Slug;
            ViewData["EventTitle"] = ev.Title;

            return View(ev);
        }
    }
}