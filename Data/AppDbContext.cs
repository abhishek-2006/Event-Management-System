using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Admin> Admins { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for Event Management System
            modelBuilder.Entity<Event>().HasData(
                new Event { 
                    EventId = 1, 
                    Title = "Tech Symposium 2026", 
                    Description = "A deep dive into AI, Machine Learning, and Cloud Computing with industry experts.", 
                    Category = "Technical", 
                    EventDate = new DateTime(2026, 03, 25, 10, 0, 0), 
                    Location = "Main Auditorium", 
                    Price = 0 
                },
                new Event { 
                    EventId = 2, 
                    Title = "Annual Sports Meet", 
                    Description = "Inter-departmental sports competition including Cricket, Football, and Athletics.", 
                    Category = "Sports", 
                    EventDate = new DateTime(2026, 04, 05, 8, 30, 0), 
                    Location = "College Ground", 
                    Price = 50 
                },
                new Event { 
                    EventId = 3, 
                    Title = "Cultural Night", 
                    Description = "A vibrant evening of dance, music, and drama performances by students.", 
                    Category = "Cultural", 
                    EventDate = new DateTime(2026, 04, 12, 18, 0, 0), 
                    Location = "Open Air Theater", 
                    Price = 150 
                },
                new Event { 
                    EventId = 4, 
                    Title = "Banking Workshop", 
                    Description = "Practical session on modern banking systems and insurance policies.", 
                    Category = "Education", 
                    EventDate = new DateTime(2026, 03, 20, 11, 0, 0), 
                    Location = "Seminar Hall B", 
                    Price = 0 
                }
            );
        }
    }
}