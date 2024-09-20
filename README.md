# **Star Fleet Builder**

Welcome to **Star Fleet Builder**! This project is an ASP.NET Core MVC application that retrieves data from the Star Wars API (SWAPI) and helps users build their own starship fleet. The application uses Entity Framework Core for database management and includes a streamlined setup process to get you started quickly.

## **Table of Contents**
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Quick Setup](#quick-setup)
  - [Windows](#windows)
- [Project Structure](#project-structure)
- [How to Use](#how-to-use)
- [Contributing](#contributing)
- [License](#license)

---

## **Features**

- Retrieve starship data from the Star Wars API (SWAPI).
- Create, update, and manage a fleet of starships.
- Simple and user-friendly UI.
- Automatic database seeding and migrations using Entity Framework Core.
- Dynamic updates to user credits after selling starships.

---

## **Technologies Used**

- **ASP.NET Core MVC**: For building the web application.
- **Entity Framework Core**: For database management.
- **Bootstrap**: Responsive UI framework.
- **SQL Server**: LocalDB for database management.
- **SWAPI**: Star Wars API to fetch starship data.

---

## **Prerequisites**

Make sure you have the following installed:

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- [SQL Server (LocalDB)](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15) or another SQL Server instance
- [Git](https://git-scm.com/) for cloning the repository

---

## **Quick Setup**

### **Windows**

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/StarFleetBuilder.git
   ```
2. **Navigate to the project directory**:
   ```bash
   cd StarFleetBuilder
   ```
3. **Run the powershell setup script**:
	- Open a PowerShell terminal in the project directory and run the following command:
   ```bash
   .\setup.ps1
   ```
This script will:
   - Check if .NET SDK is installed.
   - Install Entity Framework Core tools.
   - Restore NuGet packages.
   - Apply any pending database migrations.
   - Build and run the application.

---

## **Project Structure**
```
StarFleetBuilder/
│
├── Controllers/
│   ├── HomeController.cs  # Handles the main page
│   ├── StarshipController.cs  # Manages starship-related actions
│
├── Data/
│   ├── StarFleetBuilderContext.cs  # EF Core DbContext configuration
│   ├── DatabaseSeeder.cs  # Seeds the database with initial data
│
├── Models/
│   ├── Starship.cs  # Represents the starship model
│   ├── User.cs  # Represents the user model
│
├── Services/
│   ├── StarshipService.cs  # Handles starship business logic
│   ├── UserService.cs  # Handles user-related business logic
│
├── Views/
│   ├── Home/
│   │   └── Index.cshtml  # Main page
│   ├── Shared/
│   │   └── _Layout.cshtml  # Layout view
│
├── wwwroot/
│   ├── css/  # CSS files
│   ├── js/   # JavaScript files
│
├── appsettings.json  # Contains configuration, including connection strings
├── Program.cs  # Entry point for the application
├── setup.ps1  # PowerShell script for Windows setup
├── setup.sh  # Bash script for Linux/macOS setup
```
