using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Registration> Registrations { get; set; }
    }
}