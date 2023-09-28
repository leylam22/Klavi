using AutoMapper;
using Kalvi.Core.Entities;
using Kalvi.Core.Utilites;
using Kalvi.DataAccess.Contexts;
using Klavi.UI.Areas.Admin.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Kalvi.Core.Utilites.UserRole;

namespace Klavi.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = UserRole.Admin)]
public class CourseController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CourseController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        List<Course> courses = await _context.Courses.Include(c => c.CourseCategory).ToListAsync();
        return View(courses);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Catagories = await _context.CourseCategories.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CoursePostVM coursePost)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var catagory = _context.CourseCategories.Find(coursePost.CourseCatagoryId);

        if (catagory is null)
        {
            return BadRequest();
        }
        Course course = new();
        course.Title = coursePost.Title;
        course.Description = coursePost.Description;
        course.ImagePath = coursePost.ImagePath;
        course.CourseCategoryId = coursePost.CourseCatagoryId;
        course.Type= coursePost.Type;
        course.Price= coursePost.Price;

        course.CourseDetail = new CourseDetail
        {
            VideoPath = coursePost.VideoPath,
            Lessons = coursePost.Lessons,
            Start = coursePost.Start,
            Duration = coursePost.Duration
        };
        await _context.AddAsync(course);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        Course? Coursedb = await _context.Courses.FindAsync(id);
        if (Coursedb == null)
        {
            return NotFound();
        }
        return View(Coursedb);
    }
    [HttpPost]
    [ActionName("Delete")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Course? Coursedb = await _context.Courses.FindAsync(id);
        if (Coursedb == null)
        {
            return NotFound();
        }
        _context.Courses.Remove(Coursedb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        Course? Coursedb = await _context.Courses.Include(cd => cd.CourseDetail).FirstOrDefaultAsync(a => a.Id == id);
        //ViewBag.Catagories = await _context.CourseCategories.ToListAsync();
        ViewBag.CourseCategory = await _context.CourseCategories.ToListAsync();
        if (Coursedb is null) return NotFound();
        var CoursVm = _mapper.Map<CoursePostVM>(Coursedb);
        return View(CoursVm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int Id, Course Course)
    {
        if (Id != Course.Id)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Catagories = await _context.CourseCategories.ToListAsync();
            return View(Course);
        }
        Course? Coursedb = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);
        if (Coursedb == null)
        {
            return NotFound();
        }
        _context.Entry(Course).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
