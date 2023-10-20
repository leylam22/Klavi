using Microsoft.AspNetCore.Mvc;

namespace Klavi.UI.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
