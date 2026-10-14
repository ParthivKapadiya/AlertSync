namespace AlertSync.Web.Models;

public enum Priority
{
    Low,
    Medium,
    High
}

/// <summary>
/// How long before the due time a reminder should raise a notification.
/// The numeric value is the lead time in minutes. One month is treated as 30 days.
/// </summary>
public enum ReminderLeadTime
{
    ThirtyMinutes = 30,
    OneHour = 60,
    OneDay = 1440,
    ThreeDays = 4320,
    OneWeek = 10080,
    OneMonth = 43200
}

public enum RecurrenceType
{
    Daily,
    Weekly,
    Monthly,
    Yearly
}

public enum RelationshipType
{
    Father,
    Mother,
    Brother,
    Sister,
    Grandfather,
    Grandmother,
    Other
}

public enum DocumentType
{
    Aadhaar,
    PanCard,
    Passport,
    DrivingLicence,
    Insurance,
    Vehicle,
    Certificate,
    Medical,
    Property,
    Other
}
