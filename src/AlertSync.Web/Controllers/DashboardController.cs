using AlertSync.Web.Layout;
using AlertSync.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        var reminders = LayoutSampleCatalog.Reminders();
        var now = DateTime.Now;

        return View(new DashboardViewModel
        {
            TotalReminders = reminders.Count,
            Pending = reminders.Count(item => item.Status == "Pending"),
            Completed = reminders.Count(item => item.Status == "Completed"),
            Overdue = reminders.Count(item => item.Status == "Overdue"),
            Documents = LayoutSampleCatalog.Documents().Count,
            Today = reminders.Where(item => !item.IsCompleted && item.DueAt.Date == now.Date).OrderBy(item => item.DueAt).ToList(),
            Upcoming = reminders.Where(item => !item.IsCompleted && item.DueAt >= now).OrderBy(item => item.DueAt).Take(4).ToList(),
            Recent = reminders.OrderByDescending(item => item.CreatedAt).Take(4).ToList(),
            Notifications = LayoutSampleCatalog.Notifications().OrderByDescending(item => item.CreatedAt).Take(4).ToList()
        });
    }
}
