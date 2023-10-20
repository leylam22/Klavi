using Kalvi.DataAccess.Contexts;
using Klavi.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klavi.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM()
            {
                Courses = await _context.Courses.Include(c => c.CourseCategory).ToListAsync(),
                CourseCategories = await _context.CourseCategories.ToListAsync(),
                CourseDetails = await _context.CourseDetails.ToListAsync()
            };
            return View(vm);
        }

        public async Task<IActionResult> Detail(int id, CourseFilterVM courseFilterVM)
        {
            if (id == 0) return NotFound();
            //var course = await _context.Courses.FindAsync(id);
            var course = await _context.Courses.Include(t => t.Teachers).FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return NotFound();
            ViewBag.CourseId = course.Id;

            HomeVM vm = new()
            {
                Courses = await _context.Courses.Include(t => t.Teachers).ToListAsync(),
                CourseCategories = await _context.CourseCategories.ToListAsync(),
                CourseDetails = await _context.CourseDetails.ToListAsync(),
                Testimonials = await _context.Testimonials.Where(t=> t.Position=="student").ToListAsync(),
            };
            return View(vm);
        }
    }
}
