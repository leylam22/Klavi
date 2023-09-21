using Kalvi.DataAccess.Contexts;
using Klavi.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klavi.UI.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM()
            {
                Blogs = await _context.Blogs.ToListAsync(),
            };
            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0) return NotFound();
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            ViewBag.BlogId = blog.Id;
            HomeVM vm = new()
            {
                Blogs= await _context.Blogs.ToListAsync(),
            };
            return View(vm);
        }
    }
}
