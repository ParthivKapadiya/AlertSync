using AlertSync.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace AlertSync.Web.Controllers;

public class HelpController : Controller
{
    private readonly DatabaseDesignReader _design;

    public HelpController(DatabaseDesignReader design)
    {
        _design = design;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Database()
    {
        ViewBag.CrudPlan = CrudPlan.Items;
        return View(_design.ReadTables());
    }
}
