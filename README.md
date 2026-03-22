# EventHub — Curating Unforgettable Moments

**EventHub** is a premium, full-stack event management ecosystem. It streamlines the lifecycle of campus events, from administrative architecting to student registration and digital pass generation.

## 🎨 Brand Identity

The platform follows a **Deep Space** aesthetic, featuring:

  * **Primary Palette:** Brand Purple (`#8B5CF6`) & Brand Blue (`#3B82F6`).
  * **UI Style:** Glassmorphic cards, metallic silver accents, and high-contrast dark mode.
  * **UX Focus:** Seamless navigation, real-time filtering, and procedural digital pass generation.

---

## ✨ Key Features

### 🎓 For Students

  * **Live Catalog:** Real-time browsing of Technical, Cultural, Sports, and Educational events.
  * **Instant Registration:** Simplified enrollment with automated data validation.
  * **Digital Event Pass:** Procedurally generated boarding passes with unique security keys for gate entry.
  * **Responsive Design:** Fully optimized for mobile screens for on-the-go access.

### 🛡️ For Administrators

  * **Central Command Dashboard:** Real-time analytics showing total engagement and active event counts.
  * **Event Architect:** A high-end builder to launch, modify, or decommission campus experiences.
  * **Attendee Intelligence:** Comprehensive logs of all registered students with academic filters.
  * **Data Export:** Generate official CSV reports and formatted print views for university records.
  * **Security:** Role-based access control with a dedicated, themed Admin Portal.

---

## 🛠️ Technical Stack

  * **Framework:** ASP.NET Core MVC (C\#)
  * **Database:** Entity Framework Core (SQL Server)
  * **Styling:** Tailwind CSS 3.0
  * **Frontend Logic:** JavaScript (ES6+), Razor Syntax
  * **Icons/Graphics:** Custom SVG Library & Procedural CSS Barcoding

---

## 🚀 Getting Started

### Prerequisites

  * .NET 8.0 SDK or later
  * SQL Server (LocalDB or Express)
  * Visual Studio 2022 / VS Code

### Installation

1.  **Clone the repository**

    ```bash
    git clone https://github.com/abhishek-2006/EventManagementSystem.git
    cd EventManagementSystem
    ```

2.  **Configure Connection String**
    Update `appsettings.json` with your local database credentials.

3.  **Apply Migrations**

    ```bash
    dotnet ef database update
    ```

4.  **Run the Application**

    ```bash
    dotnet run
    ```

    Open `http://localhost:5199` to view the portal.

---

## 📁 Project Structure

```text
EventManagementSystem/
├── Controllers/         # Logic for Admin & Public Routes
├── Models/              # Database Entities (Event, Registration, Admin)
├── ViewModels/          # Data Transfer Objects for optimized views
├── Views/               # Razor Templates (Themed with Tailwind)
│   ├── Admin/           # Protected Administrative Suite
│   ├── Events/          # Public Catalog & Registration
│   └── Shared/          # Layout, Header, and Footer
└── wwwroot/             # Brand Assets (logo.jpg, favicon.jpg)
```

---

## 👨‍💻 Developer

**Made with ❤️ by Abhishek Shah**