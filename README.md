# 🚀 EventHub — Curating Unforgettable Moments

**EventHub** is a premium, full-stack event management ecosystem engineered for the campus. It digitizes the entire event lifecycle—from administrative "Architecting" to seamless student registration and procedural security pass generation.

## 🎨 Brand Identity: "Deep Space"
The platform is built on a custom design language that balances high-end aesthetics with administrative utility.
* **Core Palette:** High-vibrancy **Brand Purple** (`#8B5CF6`) and **Strategic Blue** (`#3B82F6`).
* **Visual Philosophy:** Glassmorphic containers, metallic silver accents, and a high-contrast "Deep Space" dark mode.
* **Signature UI:** Rounded-corner architecture ($3.5rem$ radius) and animated gradient transitions.

## ✨ Key Features

### 🎓 For Students (The Experience)
* **Live Catalog:** A real-time, filtered interface for Technical, Cultural, Sports, and Educational events.
* **Metallic Identity System:** Procedural student avatars based on their initial identity.
* **Elite Event Pass:** High-fidelity digital boarding passes featuring:
    * Unique **Security Auth Keys** (`EH-REG-ROLL`).
    * Procedural **CSS Barcoding** for gate verification.
    * Print-optimized layouts for physical archival.

### 🛡️ For Administrators (The Command Center)
* **Central Command Dashboard:** High-impact analytics tracking student engagement and catalog health.
* **Event Architect:** A specialized builder to launch, modify, or decommission campus experiences.
* **Attendee Intelligence:** Searchable logs with academic filters (Semester/Department).
* **Data Sovereignty:** Professional CSV export and "Official Report" print formatting for university records.
* **Session Security:** Secure Admin Portal with background-persistence prevention.

## 🛠️ Technical Stack

* **Core:** ASP.NET Core 8.0 MVC (C#)
* **Data:** Entity Framework Core (SQL Server)
* **Styling:** Tailwind CSS 3.0 (Custom JIT Configuration)
* **Interactions:** JavaScript ES6+ & Razor Templating
* **Assets:** Optimized Metallic Logo & High-Res Favicon System

## 🚀 Deployment & Local Access

### Prerequisites
* .NET 8.0 SDK
* SQL Server (Express or LocalDB)
* Visual Studio 2022 / VS Code

### Standard Installation
1.  **Clone & Enter:**
    ```bash
    git clone https://github.com/abhishek-2006/EventManagementSystem.git
    cd EventManagementSystem
    ```
2.  **Database Sync:**
    Update `appsettings.json` with your connection string, then run:
    ```bash
    dotnet ef database update
    ```
3.  **Launch:**
    ```bash
    dotnet run
    ```

### 🌐 Cross-Device Testing (Same Network)
To demo the **Mobile Pass** on a real phone while the server runs on your laptop:

1.  **Host on all Interfaces:**
    ```bash
    dotnet run --urls "http://0.0.0.0:5199"
    ```
2.  **Firewall Configuration:**
    * Open **Windows Defender Firewall** > **Advanced Settings**.
    * **Inbound Rules** > **New Rule** > **Port (TCP)**.
    * Specify Port: **5199** > **Allow Connection**.
    * Name it: `EventHub-Mobile-Demo`.
3.  **Connect:**
    Find your IPv4 (via `ipconfig`) and visit `http://[YOUR-IP]:5199` on any mobile device on the same Wi-Fi.

## 📁 Project Architecture
```text
EventManagementSystem/
├── Controllers/       # Administrative & Public Route Logic
├── Models/            # EF Core Entities (Event, Registration, Admin)
├── ViewModels/        # Optimized Data Transfer Objects
├── Views/             # High-Gloss Razor Templates
│   ├── Admin/         # Secure Management Suite
│   ├── Events/        # Discovery & Registration Flow
│   └── Shared/        # Layout Architect (Navigation & Footer)
└── wwwroot/           # Brand Identity Assets (logo.jpg, favicon.jpg)
```

## 👨‍💻 Developed By

**Made with ❤️ by Abhishek Shah**