# 🚀 EventHub — Curating Unforgettable Moments

[![.NET Version](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![MySQL](https://img.shields.io/badge/MySQL-Database-4479A1?style=for-the-badge&logo=mysql&logoColor=white)](https://www.mysql.com/)
[![TailwindCSS](https://img.shields.io/badge/Tailwind_CSS-38B2AC?style=for-the-badge&logo=tailwind-css&logoColor=white)](https://tailwindcss.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF_Core-ORM-blueviolet?style=for-the-badge&logo=nuget&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](LICENSE)

**EventHub** is a modern, full-stack campus event management web application engineered with **ASP.NET Core 10 MVC**, **Entity Framework Core**, and **MySQL**. It digitizes the entire campus event lifecycle—from administrative event orchestration, real-time discovery, and duplicate-safe student registrations to procedural digital boarding pass generation with cryptographic anti-tamper security.

---

## 📸 Screenshots

All UI screenshots and platform captures are available directly in the [`images/`](images/) directory.

---

## 🎨 Design Philosophy & Identity: "Deep Space"

The user interface balances sleek futuristic aesthetics with effortless campus utility:
* **Curated Palette:** Vibrant **Brand Purple** (`#8B5CF6`), **Strategic Blue** (`#3B82F6`), and **Metallic Slate** (`#E2E8F0`).
* **Visual Styling:** Glassmorphic navigation headers, smooth gradient backdrops, ambient glow pulses, and holographic boarding passes.
* **Theme Support:** Instant Dark/Light mode toggle with persistence and smooth transition animations.
* **Responsive Architecture:** Fully responsive mobile-friendly layouts, dynamic filter pills, and adaptive touch-friendly cards.

---

## ✨ Key Modules & Features

### 🎓 Public & Student Features
* **Dynamic Landing Page (`/`)**:
  * Hero section with ambient gradient backdrops and call-to-actions.
  * Live event showcase featuring today's and upcoming campus activities.
* **Campus Event Catalog (`/Events`)**:
  * **Categorized Sections**:
    * 🔴 **Happening Today (Live Now)** with live pulse indicator.
    * 📅 **Upcoming Events** counter.
    * 🗄️ **Past Events (Archived)**.
  * **Real-Time Client-Side Search**: Instant event filtering by title.
  * **Interactive Category Pills**: Seamlessly switch between `All`, `Technical`, `Cultural`, `Sports`, and `Education`.
* **Detailed Event Overview (`/events/{slug}`)**:
  * SEO-friendly slugged URLs.
  * Venue information, event schedule, registration timeline, and ticket pricing.
  * Real-time registration deadline calculations and status alerts (*Registration Not Started*, *Open*, or *Closed*).
* **Smart Student Registration (`/events/register/{slug}`)**:
  * Form fields: Student Full Name, Enrollment/Roll Number, Email, Phone Number, Semester, and Department.
  * Strict registration window enforcement (validates today's date against start and end dates).
  * **Anti-Duplicate Engine**: Prevents duplicate enrollment numbers from registering multiple times for the same event.
* **Elite Digital Boarding Pass / E-Ticket (`/events/RegistrationSuccess`)**:
  * **Procedural Student Avatar**: Metallic-styled avatar derived from the student's initial.
  * **Cryptographic Anti-Tamper Security Key**: SHA-256 generated checksum verification key (`EH-REG-ROLL-...`).
  * **Procedural CSS Barcode**: Gate-scan ready barcode simulation.
  * **Holographic Foil & Ambient Shimmer**: Premium card aesthetics.
  * **Print & PDF Optimization**: Clean `@media print` landscape styling hiding navigation elements for physical gate badges or PDF saves.
* **Campus Support & Inquiries (`/Home/Contact`)**:
  * Campus coordinates, support details, and inquiry channels.

---

### 🛡️ Admin Command Center Features
* **Protected Session Security (`/Admin/Login`)**:
  * Custom authorization filter (`[AdminAuth]`) protecting sensitive management routes.
  * Prevents back-button navigation caching and URL tampering.
* **Central Analytics Dashboard (`/Admin/Dashboard`)**:
  * Quick metrics tracking: Total Events Launched and Total Student Registrations.
  * Quick-launch management actions.
* **Event Architect (Full CRUD)**:
  * **Create Events (`/Admin/CreateEvent`)**: Title, category, price, event date, registration start/end dates, and location with auto-generated URL slugs.
  * **Manage Events (`/Admin/ManageEvents`)**: Central table to review, update, or decommission events.
  * **Edit Events (`/Admin/EditEvent/{slug}`)**: Real-time attribute modification with EF Core untracked entity conflict handling.
  * **Delete Events (`/Admin/DeleteEvent/{slug}`)**: Safe removal of decommissioned events.
* **Attendee Intelligence Registry (`/Admin/ViewRegistrations`)**:
  * Comprehensive attendee roster displaying student name, roll number, email, phone, semester, department, and registration timestamp.
* **Data Sovereignty & CSV Export (`/Admin/ExportAttendees`)**:
  * One-click RFC-compliant CSV report generator (`EventHub_Report_YYYYMMDD.csv`) ready for campus administration, spreadsheets, and archiving.

---

## 🛠️ Technical Stack

| Layer | Technology |
|---|---|
| **Framework** | **ASP.NET Core 10.0 (MVC)** — C# |
| **Database** | **MySQL** |
| **ORM** | **Entity Framework Core 10.0** (`MySql.EntityFrameworkCore`) |
| **Frontend Styling** | **Tailwind CSS 3.0** (Custom JIT configuration) |
| **Scripting & Interactivity** | Vanilla JavaScript (ES6+), DOM APIs |
| **Security & Hashing** | SHA-256 Cryptographic Checksums, Session State, Anti-Forgery Tokens (`[ValidateAntiForgeryToken]`) |
| **Templating** | ASP.NET Core Razor Pages (`.cshtml`) & ViewModels |

---

## ⚙️ Installation & Setup Guide

### 📋 Prerequisites
Make sure the following tools are installed on your machine:
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download) (or .NET 8.0+ SDK)
* [MySQL Server](https://dev.mysql.com/downloads/mysql/) (via MySQL Community Server, XAMPP, or MariaDB)
* [Visual Studio 2022](https://visualstudio.microsoft.com/) / [VS Code](https://code.visualstudio.com/) / [JetBrains Rider](https://www.jetbrains.com/rider/)
* [Git](https://git-scm.com/)

---

### 🚀 Step-by-Step Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/abhishek-2006/Event-Management-System.git
   cd Event-Management-System
   ```

2. **Configure Database Connection**:
   Open [`appsettings.json`](appsettings.json) and configure your MySQL credentials:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3306;Database=EMS_DB;User ID=root;Password=your_password;"
     }
   }
   ```

3. **Apply Database Migrations**:
   Run the EF Core migration command to automatically create tables and seed default campus events:
   ```bash
   dotnet ef database update
   ```

4. **Run the Application**:
   ```bash
   dotnet run
   ```
   Open your browser and navigate to:
   * **Public Platform**: `https://localhost:7214` or `http://localhost:5199` (check terminal output for exact assigned port).
   * **Admin Command Center**: `https://localhost:7214/Admin/Login`.

---

## 📱 Cross-Device Testing (Preview Digital Pass on Mobile)

To demo or test the **Mobile Boarding Pass** on your smartphone connected to the same Wi-Fi network:

1. **Run with all interface bindings**:
   ```bash
   dotnet run --urls "http://0.0.0.0:5199"
   ```

2. **Allow Inbound Port in Firewall (Windows)**:
   * Open **Windows Defender Firewall with Advanced Security**.
   * Click **Inbound Rules** > **New Rule** > Select **Port (TCP)**.
   * Enter port `5199` and select **Allow the connection**.
   * Name the rule `EventHub-Mobile-Demo`.

3. **Connect from Mobile**:
   Find your PC's local IP address (`ipconfig` on Windows, e.g., `192.168.1.15`) and visit:
   ```text
   http://192.168.1.X:5199
   ```

---

## 👨‍💻 Developed By

**Abhishek Shah**  
* 🌐 Portfolio: [abhishekshah-portfolio.vercel.app](https://abhishekshah-portfolio.vercel.app/)  
* 🐙 GitHub: [@abhishek-2006](https://github.com/abhishek-2006)  

---

## 📄 License

This project is open-source and licensed under the [MIT License](LICENSE).