using AlertSync.Web.Layout;
using AlertSync.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class CategoriesController : Controller
{
    public IActionResult Index()
    {
        return View(new CategoryPageViewModel
        {
            Categories = LayoutSampleCatalog.Categories()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind(Prefix = "Form")] CategoryFormViewModel form)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Index), new CategoryPageViewModel
            {
                Form = form,
                Categories = LayoutSampleCatalog.Categories()
            });
        }

        TempData["LayoutNotice"] = LayoutNotices.ValidForm;
        return RedirectToAction(nameof(Index));
    }
}
