using AlertSync.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlertSync.Web.Data;

public class AlertSyncDbContext : IdentityDbContext<ApplicationUser>
{
    public AlertSyncDbContext(DbContextOptions<AlertSyncDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserPreference> UserPreferences => Set<UserPreference>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Reminder> Reminders => Set<Reminder>();

    public DbSet<ReminderAlert> ReminderAlerts => Set<ReminderAlert>();

    public DbSet<UserNotification> UserNotifications => Set<UserNotification>();

    public DbSet<FamilyMember> FamilyMembers => Set<FamilyMember>();

    public DbSet<UserDocument> UserDocuments => Set<UserDocument>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AlertSyncDbContext).Assembly);
    }
}
