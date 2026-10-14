namespace AlertSync.Web.Models;

public class Reminder
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    /// <summary>Due instant stored in UTC. Screens convert it to the user's time zone.</summary>
    public DateTime DueAt { get; set; }

    public Priority Priority { get; set; }

    /// <summary>
    /// Completion is stored. Pending and Overdue are not stored:
    /// a reminder is Overdue when it is not completed and DueAt is in the past.
    /// </summary>
    public bool IsCompleted { get; set; }

    public DateTime? CompletedAt { get; set; }

    public ReminderLeadTime LeadTime { get; set; }

    public bool IsRecurring { get; set; }

    public RecurrenceType? RecurrenceType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<ReminderAlert> Alerts { get; set; } = new List<ReminderAlert>();

    public ICollection<UserNotification> Notifications { get; set; } = new List<UserNotification>();
}
