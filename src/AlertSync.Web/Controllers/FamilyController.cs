using AlertSync.Web.Layout;
using AlertSync.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class FamilyController : Controller
{
    public IActionResult Index(string? q)
    {
        var items = LayoutSampleCatalog.Family().AsEnumerable();
        if (!string.IsNullOrWhiteSpace(q))
        {
            items = items.Where(item =>
                item.Name.Contains(q.Trim(), StringComparison.OrdinalIgnoreCase)
                || item.Relationship.Contains(q.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        ViewBag.Search = q;
        return View(items.ToList());
    }

    public IActionResult Create()
    {
        return View(new FamilyFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(FamilyFormViewModel model)
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
        var member = LayoutSampleCatalog.FindFamily(id);
        if (member is null)
        {
            return View("NotFound");
        }

        return View(new FamilyFormViewModel
        {
            Id = member.Id,
            Name = member.Name,
            Relationship = member.Relationship,
            DateOfBirth = member.DateOfBirth,
            Notes = member.Notes
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, FamilyFormViewModel model)
    {
        if (LayoutSampleCatalog.FindFamily(id) is null)
        {
            return View("NotFound");
        }

        if (!ModelState.IsValid)
        {
            model.Id = id;
            return View(model);
        }

        TempData["LayoutNotice"] = LayoutNotices.ValidForm;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var member = LayoutSampleCatalog.FindFamily(id);
        if (member is null)
        {
            return View("NotFound");
        }

        var hasDocuments = LayoutSampleCatalog.Documents().Any(document => document.FamilyMember == member.Name);
        TempData["LayoutNotice"] = hasDocuments
            ? "TCIE-2: this member still has sample documents, so the plan blocks deletion. The database was not changed."
            : LayoutNotices.Confirmed;
        return RedirectToAction(nameof(Index));
    }
}
