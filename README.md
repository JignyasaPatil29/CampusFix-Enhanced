# CampusFix — Campus Maintenance and Complaint Management System

CampusFix is a web-based campus maintenance complaint tracker built with ASP.NET Core MVC, C#, Entity Framework Core, and MySQL. It helps users report, track, and manage campus maintenance issues through a responsive dashboard and complaint management interface.

## Features

- Create, view, edit, and delete complaints.
- Search complaints by title or location.
- Filter complaints by status and category.
- Dashboard metrics for total, pending, in-progress, and resolved complaints.
- Form validation and user-friendly success messages.
- Responsive Bootstrap interface.
- Enhanced dashboard design with animations and campus photography.
- Light/dark theme toggle with reduced-motion accessibility support.

## Technologies Used

- ASP.NET Core MVC
- C#
- Entity Framework Core
- MySQL
- HTML, CSS, and JavaScript
- Bootstrap

## Requirements

- .NET 8 SDK
- MySQL Server
- MySQL Workbench (optional)
- VS Code with the C# Dev Kit extension

## Setup Instructions

### 1. Create the database

In MySQL Workbench, connect to your local MySQL server and run:

```sql
CREATE DATABASE IF NOT EXISTS campusfix;
```

### 2. Configure the connection string

Open a terminal in the folder containing `CampusFix.csproj` and run:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=campusfix;User ID=root;Password=YOUR_MYSQL_PASSWORD;"
```

Replace `YOUR_MYSQL_PASSWORD` with your actual local MySQL password. Keep real credentials out of source control.

If User Secrets has already been initialized for this project, skip `dotnet user-secrets init`.

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Apply database migrations

```bash
dotnet ef migrations list
dotnet ef database update
```

If `dotnet-ef` is not installed, install the EF Core 8 tool:

```bash
dotnet tool install --global dotnet-ef --version 8.0.0
```

### 5. Run the application

```bash
dotnet run
```

Open the local URL printed in the terminal. Visit `/Complaints` to manage complaints.

## Security Notes

- Never commit your MySQL password or other secrets.
- Use .NET User Secrets for local development credentials.
- Do not commit build output such as `bin/` or `obj/`.
- This starter project does not include authentication or role-based authorization.

## Project Purpose

CampusFix demonstrates the fundamentals of MVC architecture, CRUD operations, database integration, model validation, search and filtering, session state, and responsive web design.