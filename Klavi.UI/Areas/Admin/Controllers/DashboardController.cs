using Kalvi.Core.Utilites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Klavi.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = UserRole.Admin)]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
