# CampusFix — ASP.NET Core MVC + MySQL

A beginner-friendly campus maintenance complaint tracker demonstrating MVC, EF Core, MySQL, CRUD, model validation, session state, TempData, search/filtering, and a responsive Bootstrap UI.

## Requirements
- .NET 8 SDK
- MySQL Server (and optionally MySQL Workbench)
- VS Code with C# Dev Kit extension

## 1. Create the database
In MySQL Workbench, connect to your local MySQL Server and run:

```sql
CREATE DATABASE IF NOT EXISTS campusfix;
```

## 2. Set your local connection string securely
Open a terminal in the folder containing `CampusFix.csproj`:

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=campusfix;User ID=root;Password=YOUR_MYSQL_PASSWORD;"
```

Replace `YOUR_MYSQL_PASSWORD` with the password for your local MySQL account. Do not put the real password in `appsettings.json` or commit it to GitHub. User Secrets are stored outside the project directory and are for local development.

## 3. Install EF migration tool
```powershell
dotnet tool install --global dotnet-ef --version 8.0.0
```
If already installed, use `dotnet tool update --global dotnet-ef --version 8.0.0`.

## 4. Create the tables
If `InitialCreate` already appears in `dotnet ef migrations list`, do not add it again. Apply the existing migration with `dotnet ef database update`.
```powershell
dotnet restore
dotnet ef migrations list
dotnet ef database update
```

## 5. Run the site
```powershell
dotnet run
```
Open the `http://localhost:...` or `https://localhost:...` URL printed in the terminal, then use `/Complaints` to manage records.

## Design refresh
- Responsive dashboard with animated metrics and resolution progress
- Campus photography, floating callouts, and smooth entrance animations
- Interactive complaint filters and action menu
- Persistent light/dark theme toggle
- Respects reduced-motion accessibility settings

## Features
- Complaint create, list, details, edit, delete
- Required-field and length validation
- Search by title/location and filter by status/category
- Dashboard counts for total, pending, in-progress, resolved
- Session remembers the most recently created complaint ID
- TempData success messages
- Responsive Bootstrap layout

## GitHub safety
- Never commit your MySQL password.
- The real connection string is kept in local .NET User Secrets, not in this repository.
- Do not commit `bin/`, `obj/`, or personal data.
- This starter is for local development and does not include authentication or role-based authorization.
