using Microsoft.AspNetCore.Mvc;

namespace Klavi.UI.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
