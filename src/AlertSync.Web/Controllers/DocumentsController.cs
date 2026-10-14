using AlertSync.Web.Layout;
using AlertSync.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class DocumentsController : Controller
{
    private static readonly string[] AllowedExtensions = [".pdf", ".jpg", ".jpeg", ".png"];

    public IActionResult Index(string? q, string? type, string? member)
    {
        var items = LayoutSampleCatalog.Documents().AsEnumerable();
        if (!string.IsNullOrWhiteSpace(q))
        {
            items = items.Where(item => item.Name.Contains(q.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(type))
        {
            items = items.Where(item => item.Type == type);
        }

        if (member == "personal")
        {
            items = items.Where(item => item.IsPersonal);
        }
        else if (!string.IsNullOrWhiteSpace(member))
        {
            items = items.Where(item => item.FamilyMember == member);
        }

        ViewBag.Search = q;
        ViewBag.Type = type;
        ViewBag.Member = member;
        return View(items.OrderByDescending(item => item.UploadedAt).ToList());
    }

    public IActionResult Create()
    {
        return View(new DocumentFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestSizeLimit(6 * 1024 * 1024)]
    public IActionResult Create(DocumentFormViewModel model)
    {
        if (model.File is null || model.File.Length == 0)
        {
            ModelState.AddModelError(nameof(model.File), "Choose a PDF, JPG, or PNG file.");
        }
        else
        {
            var extension = Path.GetExtension(model.File.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(nameof(model.File), "Only PDF, JPG, JPEG, and PNG files are allowed.");
            }

            if (model.File.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError(nameof(model.File), "The file must be 5 MB or smaller.");
            }
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // The uploaded bytes are discarded. Private storage is part of the final submission.
        TempData["LayoutNotice"] = LayoutNotices.ValidForm;
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var document = LayoutSampleCatalog.FindDocument(id);
        return document is null ? View("NotFound") : View(document);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (LayoutSampleCatalog.FindDocument(id) is null)
        {
            return View("NotFound");
        }

        TempData["LayoutNotice"] = LayoutNotices.Confirmed;
        return RedirectToAction(nameof(Index));
    }
}
