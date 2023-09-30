using Kalvi.DataAccess.Contexts;
using Klavi.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klavi.UI.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            TeacherVm teacherVm = new()
            {
                Teachers= await _context.Teachers.ToListAsync(),
                Testimonials = await _context.Testimonials.Where(t => t.Position == "teacher").ToListAsync()
            };
            return View(teacherVm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0) return NotFound();
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();
            ViewBag.TeacherId = teacher.Id;
            TeacherVm vm = new()
            {
                Teachers= await _context.Teachers.ToListAsync()
            };
            return View(vm);
        }
    }
}
