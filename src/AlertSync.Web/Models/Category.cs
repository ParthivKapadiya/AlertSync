namespace AlertSync.Web.Models;

public class Category
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
}
