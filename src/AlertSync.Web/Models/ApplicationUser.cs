using Microsoft.AspNetCore.Identity;

namespace AlertSync.Web.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public UserPreference? Preference { get; set; }

    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    public ICollection<UserNotification> Notifications { get; set; } = new List<UserNotification>();

    public ICollection<FamilyMember> FamilyMembers { get; set; } = new List<FamilyMember>();

    public ICollection<UserDocument> Documents { get; set; } = new List<UserDocument>();
}
