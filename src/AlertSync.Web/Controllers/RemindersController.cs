using AlertSync.Web.Layout;
using AlertSync.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class RemindersController : Controller
{
    public IActionResult Index(string? q, string? category, string? priority, string? status, string? sort)
    {
        var items = LayoutSampleCatalog.ApplyReminderFilters(
            LayoutSampleCatalog.Reminders(), q, category, priority, status, sort);

        return View(new ReminderListViewModel
        {
            Search = q,
            Category = category,
            Priority = priority,
            Status = status,
            Sort = sort == "title" ? "title" : "due",
            Items = items.ToList()
        });
    }

    public IActionResult Upcoming()
    {
        var now = DateTime.Now;
        var items = LayoutSampleCatalog.Reminders()
            .Where(item => !item.IsCompleted && item.DueAt >= now)
            .OrderBy(item => item.DueAt)
            .ToList();

        return View(items);
    }

    public IActionResult Completed(string? q, string? category)
    {
        var items = LayoutSampleCatalog.ApplyReminderFilters(
                LayoutSampleCatalog.Reminders().Where(item => item.IsCompleted),
                q,
                category,
                null,
                null,
                "due")
            .OrderByDescending(item => item.CompletedAt)
            .ToList();

        ViewBag.Search = q;
        ViewBag.Category = category;
        return View(items);
    }

    public IActionResult Create()
    {
        return View(new ReminderFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ReminderFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        TempData["LayoutNotice"] = LayoutNotices.ValidForm;
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var reminder = LayoutSampleCatalog.FindReminder(id);
        if (reminder is null)
        {
            return View("NotFound");
        }

        return View(new ReminderFormViewModel
        {
            Id = reminder.Id,
            Title = reminder.Title,
            Description = reminder.Description,
            DueDate = DateOnly.FromDateTime(reminder.DueAt),
            DueTime = TimeOnly.FromDateTime(reminder.DueAt),
            Category = reminder.Category,
            Priority = reminder.Priority,
            LeadTime = reminder.LeadTime,
            Repeat = reminder.Repeat
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, ReminderFormViewModel model)
    {
        if (LayoutSampleCatalog.FindReminder(id) is null)
        {
            return View("NotFound");
        }

        if (!ModelState.IsValid)
        {
            model.Id = id;
            return View(model);
        }

        TempData["LayoutNotice"] = LayoutNotices.ValidForm;
        return RedirectToAction(nameof(Details), new { id });
    }

    public IActionResult Details(int id)
    {
        var reminder = LayoutSampleCatalog.FindReminder(id);
        return reminder is null ? View("NotFound") : View(reminder);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Complete(int id)
    {
        if (LayoutSampleCatalog.FindReminder(id) is null)
        {
            return View("NotFound");
        }

        TempData["LayoutNotice"] = LayoutNotices.Confirmed;
        return RedirectToAction(nameof(Completed));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (LayoutSampleCatalog.FindReminder(id) is null)
        {
            return View("NotFound");
        }

        TempData["LayoutNotice"] = LayoutNotices.Confirmed;
        return RedirectToAction(nameof(Index));
    }
}
