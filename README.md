# AlertSync

Smart Reminder and Digital Asset Management System.

Course: CE545 — Enterprise Computing Through .NET Frameworks  
University: RK University  
Team: Parthiv Kapadiya, Krish Raiyani, Nandish Dholariya

This folder is the **TCIE-2** submission: connected screens, plus a normalized SQLite schema and a CRUD plan. Saving records, sign-in, and notifications are the final submission.

## What you can show

- Purple and white dashboard layout with a sidebar that links every screen.
- Login, register, and forgot-password layouts. Forms validate. They do not create accounts yet.
- Reminder list with search, filters, and sort. Details, edit, upcoming, and completed screens.
- Calendar month, week, and day views. An event opens that reminder.
- Categories, notifications, document locker, family members, profile, settings, and help.
- Database design page at `/Help/Database`. The tables are read from the Entity Framework model.
- On startup the app creates `src/AlertSync.Web/App_Data/alertsync.db` from the migration.

The lists on screen are sample layout data. A valid form shows a notice that the database was not changed.

## Prerequisites

- .NET SDK 9 (this machine uses 9.0.315)
- macOS Terminal or any terminal that can run `dotnet`

## Run

```bash
cd AlertSync
dotnet tool restore
dotnet run --project src/AlertSync.Web --launch-profile http
```

Open http://localhost:5182

Useful pages:

- Dashboard: http://localhost:5182
- Login layout: http://localhost:5182/Account/Login
- Database design: http://localhost:5182/Help/Database

`dotnet tool restore` installs the local `dotnet-ef` tool used to add migrations. The app itself does not need that tool to run. The migration is already in the project.

## Database

Connection string in `src/AlertSync.Web/appsettings.json`:

```text
Data Source=App_Data/alertsync.db
```

The app turns that into a full path inside the project and applies migrations on startup. The database file is not committed.

To recreate the migration later:

```bash
cd AlertSync
dotnet tool restore
dotnet dotnet-ef migrations add MigrationName --project src/AlertSync.Web --output-dir Data/Migrations
```

## What is not in TCIE-2

- Identity sign-in and ownership checks
- Insert, update, and delete against SQLite
- Private document files and download
- Background notifications and SignalR

Those belong to the final practical submission. The CRUD plan for them is on the database design page and in `docs/TCIE-2.md`.
