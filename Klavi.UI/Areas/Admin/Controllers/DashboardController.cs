using Microsoft.AspNetCore.Mvc;

namespace Klavi.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
