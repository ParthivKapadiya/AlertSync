using AlertSync.Web.Layout;
using AlertSync.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class SettingsController : Controller
{
    public IActionResult Index()
    {
        return View(new SettingsFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(SettingsFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        TempData["LayoutNotice"] = LayoutNotices.ValidForm;
        return RedirectToAction(nameof(Index));
    }
}
