using AlertSync.Web.Layout;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class CalendarController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Events()
    {
        // Sample events for the layout. The final endpoint will require a signed-in user
        // and return only that user's reminders.
        var events = LayoutSampleCatalog.Reminders().Select(reminder => new
        {
            id = reminder.Id,
            title = reminder.Title,
            start = reminder.DueAt.ToString("yyyy-MM-ddTHH:mm:ss"),
            url = Url.Action("Details", "Reminders", new { id = reminder.Id }),
            className = CssClassFor(reminder.Priority, reminder.IsCompleted)
        });

        return Json(events);
    }

    private static string CssClassFor(string priority, bool completed)
    {
        if (completed)
        {
            return "event-done";
        }

        return priority switch
        {
            "High" => "event-high",
            "Medium" => "event-medium",
            _ => "event-low"
        };
    }
}
