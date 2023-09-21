using Microsoft.AspNetCore.Mvc;

namespace Klavi.UI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
