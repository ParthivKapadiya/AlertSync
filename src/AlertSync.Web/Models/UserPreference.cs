namespace AlertSync.Web.Models;

public class UserPreference
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;

    public string TimeZone { get; set; } = "Asia/Kolkata";

    public bool InAppNotificationsEnabled { get; set; } = true;

    public DateTime UpdatedAt { get; set; }
}
