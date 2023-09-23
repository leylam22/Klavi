using Kalvi.DataAccess.Contexts;
using Klavi.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klavi.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM()
            {
                Courses = await _context.Courses.OrderByDescending(c => c.Created).ToListAsync(),
                CourseCategories= await _context.CourseCategories.ToListAsync(),
                Teachers=await _context.Teachers.ToListAsync(),
            };
            return View(vm);
        }
    }
}
