# 🚀 EventHub — Campus Event Management System

[![.NET Version](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![MySQL](https://img.shields.io/badge/MySQL-Database-4479A1?style=for-the-badge&logo=mysql&logoColor=white)](https://www.mysql.com/)
[![TailwindCSS](https://img.shields.io/badge/Tailwind_CSS-38B2AC?style=for-the-badge&logo=tailwind-css&logoColor=white)](https://tailwindcss.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF_Core-ORM-blueviolet?style=for-the-badge&logo=nuget&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](LICENSE)

**EventHub** is a full-stack campus event management web application built with **ASP.NET Core 10 MVC**, **Entity Framework Core**, and **MySQL**. It streamlines the entire event lifecycle on campus—from event publishing and real-time discovery to duplicate-safe student registrations and printable digital e-tickets.

Application UI captures are available in the [`images/`](images/) directory.

---

## ⚡ Core Features

### 🎓 Student & Public Portal
* **Event Discovery & Filtering:** Browse events categorized by timeline status (*Happening Today*, *Upcoming*, *Past*), with instant client-side search and category filters (*Technical, Cultural, Sports, Education*).
* **Detailed Event View:** View schedules, locations, ticket pricing, and dynamic registration window deadlines.
* **Duplicate-Safe Registration:** Form validation enforcing active registration windows and preventing duplicate enrollments for the same event.
* **Digital Boarding Pass / E-Ticket:** Generates a printable e-ticket with a verification key, barcode simulation, and print/PDF-optimized styling for badge printing.

### 🛡️ Admin Management
* **Secure Authentication:** Session-based admin login with custom authorization filters (`[AdminAuth]`) to protect management routes and prevent unauthorized access.
* **Analytics Dashboard:** Central overview of total active events and cumulative student registrations.
* **Event Management (CRUD):** Create, update, and delete campus events with auto-generated URL slugs, venue details, pricing, and registration date ranges.
* **Attendee Records & CSV Export:** View registered students with filtering and download the full registration roster as an RFC-compliant CSV file.

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| **Framework** | ASP.NET Core 10.0 MVC (C#) |
| **Database** | MySQL |
| **ORM** | Entity Framework Core 10.0 (`MySql.EntityFrameworkCore`) |
| **Frontend** | Razor Views (`.cshtml`), Tailwind CSS, Vanilla JavaScript |
| **Security** | Session State, Anti-Forgery Tokens (`[ValidateAntiForgeryToken]`), Action Filters |

---

## 🚀 Getting Started

### Prerequisites
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download) (or .NET 8.0+)
* [MySQL Server](https://dev.mysql.com/downloads/mysql/) (Community Server, XAMPP, or MariaDB)
* [Git](https://git-scm.com/)

---

### Installation & Run

1. **Clone the repository:**
   ```bash
   git clone https://github.com/abhishek-2006/Event-Management-System.git
   cd Event-Management-System
   ```

2. **Configure Database Connection:**
   Update the connection string in [`appsettings.json`](appsettings.json) with your MySQL credentials:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3306;Database=EMS_DB;User ID=root;Password=your_password;"
     }
   }
   ```

**3. Apply Database Migrations:**
   Install the EF Core CLI tool if it is not already installed, then apply the migrations to create or update the database schema.

   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```

   If `dotnet-ef` is already installed, the installation command may report that it already exists. In that case, skip the first command and run `dotnet ef database update`.

4. **Add an Admin Account (Optional):**
   To log in to the admin dashboard, insert an administrator row into the `Admins` table in MySQL:
   ```sql
   INSERT INTO Admins (Username, Email, Password) 
   VALUES ('admin', 'admin@eventhub.com', 'admin123');
   ```

5. **Start the application:**
   ```bash
   dotnet run
   ```
   Open your browser and navigate to:
   * **Public Platform:** `http://localhost:5199` (or the port shown in your terminal)
   * **Admin Command Center:** `http://localhost:5199/Admin/Login`

---

## 👨‍💻 Developed By

**Abhishek Shah**
* 🌐 Portfolio: [abhishekshah-portfolio.vercel.app](https://abhishekshah-portfolio.vercel.app/)
* 🐙 GitHub: [@abhishek-2006](https://github.com/abhishek-2006)

---

## 📄 License

This project is open-source and licensed under the [MIT License](LICENSE).