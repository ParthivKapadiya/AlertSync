using AlertSync.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlertSync.Web.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(user => user.FullName).HasMaxLength(120).IsRequired();
        builder.Property(user => user.CreatedAt).IsRequired();
    }
}

public class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
{
    public void Configure(EntityTypeBuilder<UserPreference> builder)
    {
        builder.ToTable("UserPreferences");
        builder.Property(preference => preference.TimeZone).HasMaxLength(64).IsRequired();
        builder.Property(preference => preference.UpdatedAt).IsRequired();
        builder.HasIndex(preference => preference.UserId).IsUnique();

        builder.HasOne(preference => preference.Owner)
            .WithOne(user => user.Preference)
            .HasForeignKey<UserPreference>(preference => preference.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.Property(category => category.Name).HasMaxLength(80).IsRequired();
        builder.Property(category => category.Description).HasMaxLength(300);
        builder.Property(category => category.CreatedAt).IsRequired();
        builder.HasIndex(category => new { category.UserId, category.Name }).IsUnique();

        builder.HasOne(category => category.Owner)
            .WithMany(user => user.Categories)
            .HasForeignKey(category => category.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
{
    public void Configure(EntityTypeBuilder<Reminder> builder)
    {
        builder.ToTable("Reminders");
        builder.Property(reminder => reminder.Title).HasMaxLength(150).IsRequired();
        builder.Property(reminder => reminder.Description).HasMaxLength(2000);
        builder.Property(reminder => reminder.Priority).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(reminder => reminder.LeadTime).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(reminder => reminder.RecurrenceType).HasConversion<string>().HasMaxLength(20);
        builder.Property(reminder => reminder.DueAt).IsRequired();
        builder.Property(reminder => reminder.CreatedAt).IsRequired();
        builder.Property(reminder => reminder.UpdatedAt).IsRequired();

        builder.HasIndex(reminder => new { reminder.UserId, reminder.DueAt });
        builder.HasIndex(reminder => new { reminder.UserId, reminder.IsCompleted });

        builder.HasOne(reminder => reminder.Owner)
            .WithMany(user => user.Reminders)
            .HasForeignKey(reminder => reminder.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Restrict keeps a category from disappearing while reminders still use it.
        builder.HasOne(reminder => reminder.Category)
            .WithMany(category => category.Reminders)
            .HasForeignKey(reminder => reminder.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class ReminderAlertConfiguration : IEntityTypeConfiguration<ReminderAlert>
{
    public void Configure(EntityTypeBuilder<ReminderAlert> builder)
    {
        builder.ToTable("ReminderAlerts");
        builder.Property(alert => alert.NotifyAt).IsRequired();

        // One alert per reminder and notify time. This is the duplicate guard.
        builder.HasIndex(alert => new { alert.ReminderId, alert.NotifyAt }).IsUnique();
        builder.HasIndex(alert => alert.NotifyAt);

        builder.HasOne(alert => alert.Reminder)
            .WithMany(reminder => reminder.Alerts)
            .HasForeignKey(alert => alert.ReminderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
{
    public void Configure(EntityTypeBuilder<UserNotification> builder)
    {
        builder.ToTable("UserNotifications");
        builder.Property(notification => notification.Message).HasMaxLength(500).IsRequired();
        builder.Property(notification => notification.CreatedAt).IsRequired();
        builder.HasIndex(notification => new { notification.UserId, notification.IsRead });
        builder.HasIndex(notification => notification.ReminderAlertId).IsUnique();

        builder.HasOne(notification => notification.Owner)
            .WithMany(user => user.Notifications)
            .HasForeignKey(notification => notification.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(notification => notification.Reminder)
            .WithMany(reminder => reminder.Notifications)
            .HasForeignKey(notification => notification.ReminderId)
            .OnDelete(DeleteBehavior.Cascade);

        // SetNull avoids a second cascade path from Reminder through ReminderAlert.
        // SQL Server rejects two cascade paths; SQLite would hide that problem.
        builder.HasOne(notification => notification.ReminderAlert)
            .WithOne(alert => alert.Notification)
            .HasForeignKey<UserNotification>(notification => notification.ReminderAlertId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.ToTable("FamilyMembers");
        builder.Property(member => member.Name).HasMaxLength(120).IsRequired();
        builder.Property(member => member.Relationship).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(member => member.Notes).HasMaxLength(500);
        builder.Property(member => member.CreatedAt).IsRequired();
        builder.HasIndex(member => member.UserId);

        builder.HasOne(member => member.Owner)
            .WithMany(user => user.FamilyMembers)
            .HasForeignKey(member => member.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class UserDocumentConfiguration : IEntityTypeConfiguration<UserDocument>
{
    public void Configure(EntityTypeBuilder<UserDocument> builder)
    {
        builder.ToTable("UserDocuments");
        builder.Property(document => document.DocumentName).HasMaxLength(150).IsRequired();
        builder.Property(document => document.DocumentType).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(document => document.StoredFileName).HasMaxLength(80).IsRequired();
        builder.Property(document => document.OriginalFileName).HasMaxLength(255).IsRequired();
        builder.Property(document => document.StoragePath).HasMaxLength(300).IsRequired();
        builder.Property(document => document.ContentType).HasMaxLength(100).IsRequired();
        builder.Property(document => document.Description).HasMaxLength(500);
        builder.Property(document => document.UploadedAt).IsRequired();
        builder.HasIndex(document => new { document.UserId, document.DocumentType });
        builder.HasIndex(document => document.FamilyMemberId);

        builder.HasOne(document => document.Owner)
            .WithMany(user => user.Documents)
            .HasForeignKey(document => document.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // A family member with documents cannot be deleted until those documents are moved or removed.
        builder.HasOne(document => document.FamilyMember)
            .WithMany(member => member.Documents)
            .HasForeignKey(document => document.FamilyMemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
