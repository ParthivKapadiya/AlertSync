using AlertSync.Web.Layout;
using AlertSync.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Edit()
    {
        return View(new ProfileFormViewModel
        {
            FullName = LayoutSampleCatalog.DisplayName,
            PhoneNumber = string.Empty
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProfileFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        TempData["LayoutNotice"] = LayoutNotices.ValidForm;
        return RedirectToAction(nameof(Index));
    }

    public IActionResult ChangePassword()
    {
        return View(new ChangePasswordViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        TempData["LayoutNotice"] = LayoutNotices.ValidForm;
        return RedirectToAction(nameof(Index));
    }
}
