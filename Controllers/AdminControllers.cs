using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;
using EventManagementSystem.Filters;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace EventManagementSystem.Controllers
{
    [AdminAuth]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Admin")))
            {
                return RedirectToAction("Dashboard");
            }
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string login, string password)
        {
            
            var admin = _context.Admins
                .FirstOrDefault(a => (a.Username == login || a.Email == login) && 
                a.Password == password);

            if (admin != null)
            {
                HttpContext.Session.SetString("Admin", admin.Username);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            ViewBag.TotalEvents = _context.Events.Count();
            ViewBag.TotalRegistrations = _context.Registrations.Count();
            
            var eventStats = _context.Events
                .Select(e => new EventRegistrationSummary
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Slug = e.Slug,
                    Category = e.Category,
                    EventDate = e.EventDate,
                    RegistrationsCount = _context.Registrations.Count(r => r.EventId == e.EventId)
                })
                .OrderByDescending(e => e.RegistrationsCount)
                .ToList();

            ViewBag.EventStats = eventStats;

            return View();
        }

        public IActionResult CreateEvent()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost("Admin/CreateEvent")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEvent(Event ev)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                ev.Slug = ev.Title
                .ToLower()
                .Trim()
                .Replace(" ", "-")
                .Replace("--", "-")
                .Replace(".", "")
                .Replace(",", "");
                _context.Events.Add(ev);
                _context.SaveChanges();
                
                return RedirectToAction("Dashboard");
            }

            return View(ev);
        }

        public IActionResult ManageEvents()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            var regCounts = _context.Registrations
                .GroupBy(r => r.EventId)
                .Select(g => new { EventId = g.Key, Count = g.Count() })
                .ToDictionary(x => x.EventId, x => x.Count);

            var events = _context.Events.OrderByDescending(e => e.EventDate).ToList();
            foreach (var ev in events)
            {
                ev.TotalRegistrations = regCounts.TryGetValue(ev.EventId, out var count) ? count : 0;
            }

            ViewBag.TotalRegistrationsAllEvents = _context.Registrations.Count();

            return View(events);
        }

        [HttpGet("Admin/EditEvent/{slug}")]
        public IActionResult EditEvent(string slug)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            var ev = _context.Events.FirstOrDefault(e => e.Slug == slug);
            if (ev == null)
                return NotFound();

            if (ev.RegistrationStartDate.Year < 2000)
            {
                ev.RegistrationStartDate = DateTime.Today;
            }
            if (ev.RegistrationEndDate.Year < 2000)
            {
                ev.RegistrationEndDate = ev.EventDate.Year >= 2000 ? ev.EventDate : DateTime.Today.AddDays(7);
            }
            if (ev.EventDate.Year < 2000)
            {
                ev.EventDate = DateTime.Today.AddDays(7);
            }

            return View(ev);
        }

        [HttpPost("Admin/EditEvent/{slug?}")]
        [ValidateAntiForgeryToken]
        public IActionResult EditEvent(Event ev, string? slug = null)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            if (ev.RegistrationStartDate.Year < 2000)
            {
                ev.RegistrationStartDate = DateTime.Today;
            }
            if (ev.RegistrationEndDate.Year < 2000)
            {
                ev.RegistrationEndDate = ev.EventDate.Year >= 2000 ? ev.EventDate : DateTime.Today.AddDays(7);
            }
            if (ev.EventDate.Year < 2000)
            {
                ev.EventDate = DateTime.Today.AddDays(7);
            }

            if (ModelState.IsValid)
            {
                // Fetch the tracked entity from the DB to avoid EF Core
                // "instance with the same key is already being tracked" crash.
                var existing = _context.Events.Find(ev.EventId);
                if (existing == null)
                    return NotFound();

                existing.Title                = ev.Title;
                existing.Description         = ev.Description;
                existing.Category            = ev.Category;
                existing.Location            = ev.Location;
                existing.Price               = ev.Price;
                existing.EventDate           = ev.EventDate;
                existing.RegistrationStartDate = ev.RegistrationStartDate;
                existing.RegistrationEndDate   = ev.RegistrationEndDate;

                // Regenerate slug from the (possibly updated) title.
                existing.Slug = ev.Title
                    .ToLower()
                    .Trim()
                    .Replace(" ", "-")
                    .Replace("--", "-")
                    .Replace(".", "-")
                    .Replace(",", "-");

                _context.SaveChanges();

                return RedirectToAction("ManageEvents");
            }

            return View(ev);
        }

        [HttpGet("Admin/DeleteEvent/{slug}")]
        public IActionResult DeleteEvent(string slug)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            var ev = _context.Events.FirstOrDefault(e => e.Slug == slug);
            if (ev == null)
                return NotFound();

            _context.Events.Remove(ev);
            _context.SaveChanges();
            
            return RedirectToAction("ManageEvents");
        }

        [HttpGet("Admin/ViewRegistrations")]
        public IActionResult ViewRegistrations(int? eventId = null, string? slug = null)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            // Resolve event by slug if eventId is null but slug is provided
            if (!eventId.HasValue && !string.IsNullOrEmpty(slug))
            {
                var evBySlug = _context.Events.FirstOrDefault(e => e.Slug == slug);
                if (evBySlug != null)
                {
                    eventId = evBySlug.EventId;
                }
            }

            var events = _context.Events.OrderBy(e => e.Title).ToList();
            var regCounts = _context.Registrations
                .GroupBy(r => r.EventId)
                .Select(g => new { EventId = g.Key, Count = g.Count() })
                .ToDictionary(x => x.EventId, x => x.Count);

            foreach (var ev in events)
            {
                ev.TotalRegistrations = regCounts.TryGetValue(ev.EventId, out var count) ? count : 0;
            }

            var query = _context.Registrations.Include(r => r.Event).AsQueryable();

            Event? selectedEvent = null;
            if (eventId.HasValue && eventId.Value > 0)
            {
                query = query.Where(r => r.EventId == eventId.Value);
                selectedEvent = events.FirstOrDefault(e => e.EventId == eventId.Value);
            }

            var registrations = query
                .OrderByDescending(r => r.RegisteredAt)
                .Select(r => new RegistrationViewModel
                {
                    RegistrationId = r.Id,
                    EventId = r.EventId,
                    EventSlug = r.Event != null ? r.Event.Slug : null,
                    EventTitle = r.Event != null ? r.Event.Title : "Unknown Event",
                    Name = r.StudentName,
                    Phone = r.PhoneNumber,
                    Department = r.Department,
                    Semester = r.Semester,
                    Email = r.Email,
                    EnrollNumber = r.EnrollNumber,
                    RegisteredAt = r.RegisteredAt,
                    EventDate = r.Event != null ? r.Event.EventDate : DateTime.MinValue,
                    RegistrationStartDate = r.Event != null ? r.Event.RegistrationStartDate : DateTime.MinValue,
                    RegistrationEndDate = r.Event != null ? r.Event.RegistrationEndDate : DateTime.MinValue
                })
                .ToList();

            ViewBag.Events = events;
            ViewBag.SelectedEventId = eventId;
            ViewBag.SelectedEvent = selectedEvent;
            ViewBag.OverallTotalRegistrations = _context.Registrations.Count();
            ViewBag.FilteredTotalRegistrations = registrations.Count;

            return View(registrations);
        }

        [HttpGet("Admin/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Admin/ExportAttendees")]
        public IActionResult ExportAttendees(int? eventId = null)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            var query = _context.Registrations.Include(r => r.Event).AsQueryable();

            Event? selectedEvent = null;
            if (eventId.HasValue && eventId.Value > 0)
            {
                query = query.Where(r => r.EventId == eventId.Value);
                selectedEvent = _context.Events.Find(eventId.Value);
            }

            // Fetch registrations with Event details
            var data = query
                .OrderByDescending(r => r.RegisteredAt)
                .Select(r => new {
                    r.StudentName,
                    r.EnrollNumber,
                    r.Email,
                    r.PhoneNumber,
                    r.Department,
                    r.Semester,
                    Event = r.Event!.Title,
                    EventDate = r.Event!.EventDate.ToString("yyyy-MM-dd"),
                    RegStart = r.Event!.RegistrationStartDate.ToString("yyyy-MM-dd"),
                    RegEnd = r.Event!.RegistrationEndDate.ToString("yyyy-MM-dd"),
                    Date = r.RegisteredAt.ToString("yyyy-MM-dd HH:mm")
                }).ToList();

            // Create the CSV Header
            var csv = new StringBuilder();
            csv.AppendLine("Student Name,Roll Number,Email,Phone,Department,Semester,Event Name,Event Date,Reg. Start Date,Reg. End Date,Registration Date");

            // Add rows
            foreach (var row in data)
            {
                csv.AppendLine($"\"{row.StudentName}\",\"{row.EnrollNumber}\",\"{row.Email}\",\"{row.PhoneNumber}\",\"{row.Department}\",\"{row.Semester}\",\"{row.Event}\",\"{row.EventDate}\",\"{row.RegStart}\",\"{row.RegEnd}\",\"{row.Date}\"");
            }

            // Return as a downloadable file
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            var filePrefix = selectedEvent != null ? $"EventHub_{selectedEvent.Slug}" : "EventHub_All";
            return File(buffer, "text/csv", $"{filePrefix}_Report_{DateTime.Now:yyyyMMdd}.csv");
        }
    }
}