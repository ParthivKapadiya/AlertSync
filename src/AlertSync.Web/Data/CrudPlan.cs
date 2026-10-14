namespace AlertSync.Web.Data;

public sealed record CrudPlanItem(
    string Entity,
    string Create,
    string Read,
    string Update,
    string Delete,
    string Rule);

/// <summary>
/// CRUD plan for TCIE-2. The screens exist and are linked.
/// Writing these rows to SQLite is the final submission, not this one.
/// </summary>
public static class CrudPlan
{
    public static IReadOnlyList<CrudPlanItem> Items { get; } =
    [
        new(
            "ApplicationUser",
            "Register",
            "Profile",
            "Edit profile and change password",
            "Not offered. Deleting a user would remove every owned row.",
            "Email is unique. Passwords are hashed by ASP.NET Core Identity in the final submission."),
        new(
            "UserPreference",
            "Created with the account",
            "Settings",
            "Settings form",
            "Removed with the user",
            "One settings row per user."),
        new(
            "Category",
            "Categories page",
            "Categories and the reminder form",
            "Rename and description",
            "Allowed only when no reminder uses it",
            "Category name is unique for each user."),
        new(
            "Reminder",
            "Add reminder",
            "My Tasks, details, calendar, upcoming, completed, dashboard",
            "Edit reminder, mark completed",
            "Delete after confirmation",
            "Only the owner can open the row. Overdue is calculated, not stored."),
        new(
            "ReminderAlert",
            "Created when a reminder is saved",
            "Used by the scheduler, not shown as its own page",
            "Mark processed after a notification is created",
            "Removed with the reminder",
            "Unique on reminder and notify time, so the same alert cannot be inserted twice."),
        new(
            "UserNotification",
            "Created when an alert becomes due",
            "Notifications page and the top-bar count",
            "Mark read, or mark all read",
            "Delete one notification",
            "One notification per alert. The final submission pushes it with SignalR while the browser is open."),
        new(
            "FamilyMember",
            "Add family member",
            "Family list and document filter",
            "Edit name, relationship, date of birth, notes",
            "Blocked while documents still point at the member",
            "A family member is not a login. The row belongs to one user."),
        new(
            "UserDocument",
            "Upload document",
            "Document locker, details, download",
            "Metadata can be corrected. The stored file name does not change.",
            "Delete the database row and the private file together",
            "Personal documents have no family member. Files stay outside wwwroot.")
    ];
}
