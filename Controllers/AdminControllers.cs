using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;
using EventManagementSystem.Filters;
using Microsoft.AspNetCore.Authorization;

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

            var events = _context.Events.ToList();
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

            return View(ev);
        }

        [HttpPost("Admin/EditEvent")]
        [ValidateAntiForgeryToken]
        public IActionResult EditEvent(Event ev)
            {
                if (HttpContext.Session.GetString("Admin") == null)
                    return RedirectToAction("Login");

                if (ModelState.IsValid)
                {
                    ev.Slug = ev.Title
                        .ToLower()
                        .Trim()
                        .Replace(" ","-")
                        .Replace("--", "-")
                        .Replace(".", "-")
                        .Replace(",", "-");

                    _context.Events.Update(ev);
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
        public IActionResult ViewRegistrations()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            var registrations = _context.Registrations
                .Select(r => new RegistrationViewModel
                {
                    RegistrationId = r.Id,
                    EventTitle = r.Event.Title,
                    Name = r.StudentName,
                    Phone = r.PhoneNumber,
                    Department = r.Department,
                    Semester = r.Semester,
                    Email = r.Email,
                    RegisteredAt = r.RegisteredAt
                })
                .ToList();

            return View(registrations);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}