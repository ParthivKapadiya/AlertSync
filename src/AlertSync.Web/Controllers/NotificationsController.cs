using AlertSync.Web.Layout;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class NotificationsController : Controller
{
    public IActionResult Index()
    {
        return View(LayoutSampleCatalog.Notifications());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult MarkRead(int id)
    {
        TempData["LayoutNotice"] = LayoutNotices.Confirmed;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult MarkAllRead()
    {
        TempData["LayoutNotice"] = LayoutNotices.Confirmed;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        TempData["LayoutNotice"] = LayoutNotices.Confirmed;
        return RedirectToAction(nameof(Index));
    }
}
