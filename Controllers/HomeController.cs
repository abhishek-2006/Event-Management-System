using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;
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
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        // Fetch up to 3 live events happening today
        var liveEvents = _context.Events
                                .Where(e => e.EventDate >= today && e.EventDate < tomorrow)
                                .OrderBy(e => e.EventDate)
                                .Take(3)
                                .ToList();

        // Fetch up to 3 past events (most recent first)
        var pastEvents = _context.Events
                                .Where(e => e.EventDate < today)
                                .OrderByDescending(e => e.EventDate)
                                .Take(3)
                                .ToList();

        var viewModel = new HomeViewModel
        {
            LiveEvents = liveEvents,
            PastEvents = pastEvents
        };

        return View(viewModel);
    }

    [HttpGet("Contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost("Contact")]
    [ValidateAntiForgeryToken]
    public IActionResult Contact(string fullname, string email, string subject, string message)
    {
        if (ModelState.IsValid)
        {
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
