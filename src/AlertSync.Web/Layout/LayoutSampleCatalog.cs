namespace AlertSync.Web.Layout;

public sealed class ReminderSample
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public DateTime DueAt { get; init; }

    public string Category { get; init; } = string.Empty;

    public string Priority { get; init; } = string.Empty;

    public string LeadTime { get; init; } = string.Empty;

    public string Repeat { get; init; } = "Does not repeat";

    public bool IsCompleted { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? CompletedAt { get; init; }

    public string Status => IsCompleted ? "Completed" : DueAt < DateTime.Now ? "Overdue" : "Pending";
}

public sealed class DocumentSample
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;

    public string? FamilyMember { get; init; }

    public string FileLabel { get; init; } = string.Empty;

    public string SizeLabel { get; init; } = string.Empty;

    public DateTime UploadedAt { get; init; }

    public string Description { get; init; } = string.Empty;

    public bool IsPersonal => FamilyMember is null;
}

public sealed class FamilySample
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Relationship { get; init; } = string.Empty;

    public DateOnly? DateOfBirth { get; init; }

    public string Notes { get; init; } = string.Empty;
}

public sealed class NotificationSample
{
    public int Id { get; init; }

    public string Message { get; init; } = string.Empty;

    public int ReminderId { get; init; }

    public string ReminderTitle { get; init; } = string.Empty;

    public bool IsRead { get; init; }

    public DateTime CreatedAt { get; init; }
}

public sealed class CategorySample
{
    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public int ReminderCount { get; init; }
}

/// <summary>
/// Sample rows for the TCIE-2 screens. They are rebuilt on each request and are not stored in SQLite.
/// Counts on the dashboard are calculated from this same list.
/// </summary>
public static class LayoutSampleCatalog
{
    public const string DisplayName = "Parthiv Kapadiya";

    public const string Email = "parthiv.kapadiya@example.com";

    public static IReadOnlyList<string> CategoryNames { get; } =
    [
        "College", "Assignment", "Exam", "Project", "Personal", "Insurance", "Warranty",
        "EMI / Installment", "Vehicle", "Subscription", "Health", "Documents", "Other"
    ];

    public static IReadOnlyList<string> Priorities { get; } = ["Low", "Medium", "High"];

    public static IReadOnlyList<string> Statuses { get; } = ["Pending", "Completed", "Overdue"];

    public static IReadOnlyList<string> LeadTimes { get; } =
    [
        "30 minutes before", "1 hour before", "1 day before", "3 days before", "1 week before", "1 month before"
    ];

    public static IReadOnlyList<string> Repeats { get; } =
    [
        "Does not repeat", "Daily", "Weekly", "Monthly", "Yearly"
    ];

    public static IReadOnlyList<string> Relationships { get; } =
    [
        "Father", "Mother", "Brother", "Sister", "Grandfather", "Grandmother", "Other"
    ];

    public static IReadOnlyList<string> DocumentTypes { get; } =
    [
        "Aadhaar", "PAN Card", "Passport", "Driving Licence", "Insurance", "Vehicle",
        "Certificate", "Medical", "Property", "Other"
    ];

    public static IReadOnlyList<string> TimeZones { get; } =
    [
        "Asia/Kolkata", "Asia/Dubai", "Europe/London", "America/New_York"
    ];

    public static IReadOnlyList<ReminderSample> Reminders()
    {
        var now = DateTime.Now;
        return
        [
            new ReminderSample
            {
                Id = 1,
                Title = "Vehicle insurance renewal",
                Description = "Sample layout card for a motor insurance due date.",
                DueAt = LaterToday(now),
                Category = "Insurance",
                Priority = "High",
                LeadTime = "3 days before",
                Repeat = "Yearly",
                CreatedAt = now.AddDays(-12)
            },
            new ReminderSample
            {
                Id = 2,
                Title = "Vehicle PUC expiry",
                Description = "Sample layout card for a pollution certificate that is already due.",
                DueAt = now.AddDays(-3).Date.AddHours(18),
                Category = "Vehicle",
                Priority = "High",
                LeadTime = "1 week before",
                CreatedAt = now.AddDays(-20)
            },
            new ReminderSample
            {
                Id = 3,
                Title = "DBMS assignment submission",
                Description = "Sample layout card for a college assignment deadline.",
                DueAt = now.AddDays(2).Date.AddHours(17),
                Category = "Assignment",
                Priority = "Medium",
                LeadTime = "1 day before",
                CreatedAt = now.AddDays(-4)
            },
            new ReminderSample
            {
                Id = 4,
                Title = "Laptop EMI installment",
                Description = "Sample layout card for a recurring installment.",
                DueAt = now.AddDays(5).Date.AddHours(10),
                Category = "EMI / Installment",
                Priority = "High",
                LeadTime = "3 days before",
                Repeat = "Monthly",
                CreatedAt = now.AddDays(-40)
            },
            new ReminderSample
            {
                Id = 5,
                Title = "Laptop warranty expiry",
                Description = "Sample layout card for a product warranty.",
                DueAt = now.AddDays(20).Date.AddHours(12),
                Category = "Warranty",
                Priority = "Low",
                LeadTime = "1 month before",
                CreatedAt = now.AddDays(-8)
            },
            new ReminderSample
            {
                Id = 6,
                Title = "Dental checkup",
                Description = "Sample layout card for a health appointment.",
                DueAt = now.AddDays(8).Date.AddHours(11),
                Category = "Health",
                Priority = "Medium",
                LeadTime = "1 day before",
                CreatedAt = now.AddDays(-2)
            },
            new ReminderSample
            {
                Id = 7,
                Title = "Mini project review",
                Description = "Sample completed reminder for the completed-tasks screen.",
                DueAt = now.AddDays(-12).Date.AddHours(15),
                Category = "Project",
                Priority = "Medium",
                LeadTime = "1 day before",
                IsCompleted = true,
                CreatedAt = now.AddDays(-30),
                CompletedAt = now.AddDays(-11)
            },
            new ReminderSample
            {
                Id = 8,
                Title = "Cloud storage subscription",
                Description = "Sample completed reminder for a subscription renewal.",
                DueAt = now.AddDays(-20).Date.AddHours(9),
                Category = "Subscription",
                Priority = "Low",
                LeadTime = "1 week before",
                Repeat = "Yearly",
                IsCompleted = true,
                CreatedAt = now.AddDays(-50),
                CompletedAt = now.AddDays(-19)
            }
        ];
    }

    public static IReadOnlyList<FamilySample> Family()
    {
        return
        [
            new FamilySample
            {
                Id = 1,
                Name = "Harish Mehta",
                Relationship = "Father",
                DateOfBirth = new DateOnly(1976, 4, 12),
                Notes = "Fictional sample for the layout. Not a real person."
            },
            new FamilySample
            {
                Id = 2,
                Name = "Lata Mehta",
                Relationship = "Mother",
                DateOfBirth = new DateOnly(1979, 9, 3),
                Notes = "Fictional sample for the layout. Not a real person."
            },
            new FamilySample
            {
                Id = 3,
                Name = "Aarav Mehta",
                Relationship = "Brother",
                DateOfBirth = new DateOnly(2004, 1, 19),
                Notes = "Fictional sample for the layout. Not a real person."
            }
        ];
    }

    public static IReadOnlyList<DocumentSample> Documents()
    {
        var now = DateTime.Now;
        return
        [
            new DocumentSample
            {
                Id = 1,
                Name = "Motor insurance copy",
                Type = "Insurance",
                FileLabel = "sample-insurance.pdf",
                SizeLabel = "240 KB",
                UploadedAt = now.AddDays(-6),
                Description = "Fictional sample card. No real policy is stored."
            },
            new DocumentSample
            {
                Id = 2,
                Name = "Semester marksheet",
                Type = "Certificate",
                FileLabel = "sample-marksheet.pdf",
                SizeLabel = "180 KB",
                UploadedAt = now.AddDays(-15),
                Description = "Fictional sample card. No real marksheet is stored."
            },
            new DocumentSample
            {
                Id = 3,
                Name = "Driving licence copy",
                Type = "Driving Licence",
                FamilyMember = "Harish Mehta",
                FileLabel = "sample-licence.jpg",
                SizeLabel = "420 KB",
                UploadedAt = now.AddDays(-9),
                Description = "Fictional family document card. No real licence is stored."
            },
            new DocumentSample
            {
                Id = 4,
                Name = "Clinic visit note",
                Type = "Medical",
                FamilyMember = "Lata Mehta",
                FileLabel = "sample-clinic.pdf",
                SizeLabel = "96 KB",
                UploadedAt = now.AddDays(-2),
                Description = "Fictional family document card. No real medical record is stored."
            }
        ];
    }

    public static IReadOnlyList<NotificationSample> Notifications()
    {
        var now = DateTime.Now;
        var reminders = Reminders();
        return
        [
            Notice(1, reminders[0], "Vehicle insurance renewal is due today.", false, now.AddHours(-2)),
            Notice(2, reminders[1], "Vehicle PUC expiry is overdue.", false, now.AddHours(-5)),
            Notice(3, reminders[2], "DBMS assignment submission is coming up.", true, now.AddDays(-1)),
            Notice(4, reminders[4], "Laptop warranty expiry was added to your reminders.", true, now.AddDays(-8))
        ];
    }

    public static int UnreadCount => Notifications().Count(item => !item.IsRead);

    public static ReminderSample? FindReminder(int id) => Reminders().FirstOrDefault(item => item.Id == id);

    public static DocumentSample? FindDocument(int id) => Documents().FirstOrDefault(item => item.Id == id);

    public static FamilySample? FindFamily(int id) => Family().FirstOrDefault(item => item.Id == id);

    public static IReadOnlyList<CategorySample> Categories()
    {
        var reminders = Reminders();
        var described = new Dictionary<string, string>
        {
            ["College"] = "Classes, labs, and campus dates.",
            ["Assignment"] = "Subject submissions and tutorials.",
            ["Exam"] = "Internal and external exams.",
            ["Project"] = "Project reviews and viva dates.",
            ["Personal"] = "Personal plans that are not in another group.",
            ["Insurance"] = "Policy renewals for people or vehicles.",
            ["Warranty"] = "Product warranty end dates.",
            ["EMI / Installment"] = "Loan and installment dates.",
            ["Vehicle"] = "PUC, registration, and servicing.",
            ["Subscription"] = "Renewals for apps and services.",
            ["Health"] = "Appointments and checkups.",
            ["Documents"] = "Dates tied to a stored document.",
            ["Other"] = "Anything that does not fit the groups above."
        };

        return CategoryNames
            .Select(name => new CategorySample
            {
                Name = name,
                Description = described[name],
                ReminderCount = reminders.Count(reminder => reminder.Category == name)
            })
            .ToList();
    }

    public static IEnumerable<ReminderSample> ApplyReminderFilters(
        IEnumerable<ReminderSample> source,
        string? search,
        string? category,
        string? priority,
        string? status,
        string? sort)
    {
        var query = source;
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(item => item.Title.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(item => item.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(priority))
        {
            query = query.Where(item => item.Priority == priority);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(item => item.Status == status);
        }

        return sort == "title"
            ? query.OrderBy(item => item.Title)
            : query.OrderBy(item => item.DueAt);
    }

    private static DateTime LaterToday(DateTime now)
    {
        var latestToday = now.Date.AddHours(23).AddMinutes(30);
        if (now >= latestToday)
        {
            return now.AddMinutes(20);
        }

        var later = now.AddHours(3);
        return later <= latestToday ? later : latestToday;
    }

    private static NotificationSample Notice(
        int id,
        ReminderSample reminder,
        string message,
        bool isRead,
        DateTime createdAt)
    {
        return new NotificationSample
        {
            Id = id,
            ReminderId = reminder.Id,
            ReminderTitle = reminder.Title,
            Message = message,
            IsRead = isRead,
            CreatedAt = createdAt
        };
    }
}
