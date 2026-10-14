namespace AlertSync.Web.Models;

public class UserNotification
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;

    public int ReminderId { get; set; }

    public Reminder Reminder { get; set; } = null!;

    public int? ReminderAlertId { get; set; }

    public ReminderAlert? ReminderAlert { get; set; }

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ReadAt { get; set; }
}
