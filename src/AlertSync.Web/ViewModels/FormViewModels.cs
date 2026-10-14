using System.ComponentModel.DataAnnotations;

namespace AlertSync.Web.ViewModels;

public class ReminderFormViewModel
{
    public int? Id { get; set; }

    [Required, StringLength(150)]
    public string? Title { get; set; }

    [StringLength(2000)]
    public string? Description { get; set; }

    [Required]
    [Display(Name = "Due date")]
    [DataType(DataType.Date)]
    public DateOnly? DueDate { get; set; }

    [Required]
    [Display(Name = "Due time")]
    [DataType(DataType.Time)]
    public TimeOnly? DueTime { get; set; }

    [Required]
    public string? Category { get; set; }

    [Required]
    public string? Priority { get; set; }

    [Required]
    [Display(Name = "Remind me")]
    public string? LeadTime { get; set; }

    [Required]
    public string? Repeat { get; set; } = "Does not repeat";
}

public class ReminderListViewModel
{
    public string? Search { get; set; }

    public string? Category { get; set; }

    public string? Priority { get; set; }

    public string? Status { get; set; }

    public string Sort { get; set; } = "due";

    public IReadOnlyList<Layout.ReminderSample> Items { get; set; } = [];
}

public class CategoryFormViewModel
{
    [Required, StringLength(80)]
    public string? Name { get; set; }

    [StringLength(300)]
    public string? Description { get; set; }
}

public class CategoryPageViewModel
{
    public CategoryFormViewModel Form { get; set; } = new();

    public IReadOnlyList<Layout.CategorySample> Categories { get; set; } = [];
}

public class DocumentFormViewModel
{
    [Required, StringLength(150)]
    [Display(Name = "Document name")]
    public string? DocumentName { get; set; }

    [Required]
    [Display(Name = "Document type")]
    public string? DocumentType { get; set; }

    [Display(Name = "Family member")]
    public string? FamilyMember { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Display(Name = "File")]
    public IFormFile? File { get; set; }
}

public class FamilyFormViewModel
{
    public int? Id { get; set; }

    [Required, StringLength(120)]
    public string? Name { get; set; }

    [Required]
    public string? Relationship { get; set; }

    [Display(Name = "Date of birth")]
    [DataType(DataType.Date)]
    public DateOnly? DateOfBirth { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}

public class LoginViewModel
{
    [Required, EmailAddress, StringLength(256)]
    public string? Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}

public class RegisterViewModel
{
    [Required, StringLength(120)]
    [Display(Name = "Full name")]
    public string? FullName { get; set; }

    [Required, EmailAddress, StringLength(256)]
    public string? Email { get; set; }

    [Required, DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "Use at least 8 characters, including a letter and a number.")]
    public string? Password { get; set; }

    [Required, DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Display(Name = "Confirm password")]
    public string? ConfirmPassword { get; set; }
}

public class ForgotPasswordViewModel
{
    [Required, EmailAddress, StringLength(256)]
    public string? Email { get; set; }
}

public class ProfileFormViewModel
{
    [Required, StringLength(120)]
    [Display(Name = "Full name")]
    public string? FullName { get; set; }

    [Phone, StringLength(20)]
    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; set; }
}

public class ChangePasswordViewModel
{
    [Required, DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string? CurrentPassword { get; set; }

    [Required, DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "Use at least 8 characters, including a letter and a number.")]
    [Display(Name = "New password")]
    public string? NewPassword { get; set; }

    [Required, DataType(DataType.Password)]
    [Compare(nameof(NewPassword))]
    [Display(Name = "Confirm new password")]
    public string? ConfirmPassword { get; set; }
}

public class SettingsFormViewModel
{
    [Required]
    [Display(Name = "Time zone")]
    public string? TimeZone { get; set; } = "Asia/Kolkata";

    [Display(Name = "In-app notifications")]
    public bool InAppNotificationsEnabled { get; set; } = true;
}

public class DashboardViewModel
{
    public int TotalReminders { get; set; }

    public int Pending { get; set; }

    public int Completed { get; set; }

    public int Overdue { get; set; }

    public int Documents { get; set; }

    public IReadOnlyList<Layout.ReminderSample> Today { get; set; } = [];

    public IReadOnlyList<Layout.ReminderSample> Upcoming { get; set; } = [];

    public IReadOnlyList<Layout.ReminderSample> Recent { get; set; } = [];

    public IReadOnlyList<Layout.NotificationSample> Notifications { get; set; } = [];
}
