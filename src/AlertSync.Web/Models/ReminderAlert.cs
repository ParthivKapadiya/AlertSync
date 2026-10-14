namespace AlertSync.Web.Models;

/// <summary>
/// One scheduled notification time for a reminder.
/// The user is not stored here because it always comes from the parent reminder.
/// A unique pair of reminder and notify time stops duplicate alerts.
/// </summary>
public class ReminderAlert
{
    public int Id { get; set; }

    public int ReminderId { get; set; }

    public Reminder Reminder { get; set; } = null!;

    public DateTime NotifyAt { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public UserNotification? Notification { get; set; }
}
