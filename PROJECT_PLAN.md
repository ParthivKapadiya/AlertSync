# AlertSync project plan

TCIE-2 is the current stop. The final submission continues from this layout and this schema.

## Done for TCIE-2

- ASP.NET Core MVC on .NET 9, SQLite, Entity Framework Core migration
- Shared purple-and-white dashboard layout and a separate sign-in layout
- Connected screens for reminders, calendar, documents, family, profile, settings, and help
- Normalized tables, keys, delete rules, and a CRUD plan
- Sample data only on the screens, so the layout can be reviewed without pretending rows were saved

## Left for the final submission

1. ASP.NET Core Identity: register, login, forgot password, change password
2. Save reminders, categories, family members, and settings for the signed-in user
3. Calendar events from that user's reminders
4. Background service that creates a notification once per `ReminderAlert`
5. SignalR for browsers that are open
6. Private document upload, download, and delete
7. Tests for ownership, overdue calculation, upload rules, and duplicate alerts

The screen layout from TCIE-2 stays. Later work fills the same forms and lists from the database.
