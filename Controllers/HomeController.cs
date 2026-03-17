using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Models;
using EventManagementSystem.Data;
using System.Linq;

namespace EventManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Fetch top 3 upcoming events for the "Featured" section
        var featuredEvents = _context.Events
                                    .Where(e => e.EventDate >= DateTime.Now)
                                    .OrderBy(e => e.EventDate)
                                    .Take(3)
                                    .ToList();

        return View(featuredEvents);
    }

    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Contact(string fullname, string email, string subject, string message)
    {
        if (ModelState.IsValid)
        {
            // Process the contact form submission
            // For example, save to database or send email
            TempData["SuccessMessage"] = "Your message has been sent to The Hub!";
            return RedirectToAction(nameof(Contact));
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
