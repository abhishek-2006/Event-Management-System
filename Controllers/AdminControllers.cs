using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data;
using System.Linq;

namespace EventManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
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

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}