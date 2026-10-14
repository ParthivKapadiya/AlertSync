using AlertSync.Web.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuredConnection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=App_Data/alertsync.db";
var sqlite = new SqliteConnectionStringBuilder(configuredConnection);
if (!Path.IsPathRooted(sqlite.DataSource))
{
    sqlite.DataSource = Path.Combine(builder.Environment.ContentRootPath, sqlite.DataSource);
}

var databaseDirectory = Path.GetDirectoryName(sqlite.DataSource);
if (!string.IsNullOrEmpty(databaseDirectory))
{
    Directory.CreateDirectory(databaseDirectory);
}

builder.Services.AddDbContext<AlertSyncDbContext>(options =>
    options.UseSqlite(sqlite.ConnectionString));
builder.Services.AddScoped<DatabaseDesignReader>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AlertSyncDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Dashboard}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
