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
        List<Course> courses = await _context.Courses.Include(c => c.CourseCategory).Include(t => t.Teachers).ToListAsync();
        return View(courses);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Catagories = await _context.CourseCategories.ToListAsync();
        ViewBag.Teachers = await _context.Teachers.ToListAsync();
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
        var teacher = _context.Teachers.Find(coursePost.TeachersId);

        if (teacher is null) return BadRequest();
        if (catagory is null) return BadRequest();

        Course course = new();
        course.Title = coursePost.Title;
        course.Description = coursePost.Description;
        course.ImagePath = coursePost.ImagePath;
        course.CourseCategoryId = coursePost.CourseCatagoryId;
        course.Type= coursePost.Type;
        course.Price= coursePost.Price;
        course.TeachersId = coursePost.TeachersId;

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
        Course? Coursedb = await _context.Courses
            .Include(cd => cd.CourseDetail)
            .Include(t=> t.Teachers)
            .FirstOrDefaultAsync(a => a.Id == id);

        ViewBag.CourseCategory = await _context.CourseCategories.ToListAsync();
        ViewBag.Teachers = await _context.Teachers.ToListAsync();

        if (Coursedb is null) return NotFound();
        //var CoursVm = _mapper.Map<CoursePostVM>(Coursedb);
        CoursePostVM courseVm = new CoursePostVM
        {
            //Id = Coursedb.Id,
            Title = Coursedb.Title,
            Description = Coursedb.Description,
            // Map other properties from Course to CoursePostVM here

            CourseCatagoryId = Coursedb.CourseCategoryId,
            TeachersId = Coursedb.TeachersId,
            // Map other properties from Course to CoursePostVM here

            // Manually populate properties from CourseDetail
            Duration = Coursedb.CourseDetail.Duration,
            Price = Coursedb.Price,
            VideoPath = Coursedb.CourseDetail.VideoPath,
            Lessons= Coursedb.CourseDetail.Lessons,
            ImagePath= Coursedb.ImagePath,
            Type = Coursedb.Type,
            Start= Coursedb.CourseDetail.Start
            // Map other CourseDetail properties here
        };
        return View(courseVm);
    }
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Update(int Id, Course Course)
    //{
    //    if (Id != Course.Id)
    //    {
    //        return BadRequest();
    //    }
    //    if (!ModelState.IsValid)
    //    {
    //        ViewBag.Catagories = await _context.CourseCategories.ToListAsync();
    //        ViewBag.Teachers = await _context.Teachers.ToListAsync();
    //        return View(Course);
    //    }
    //    Course? Coursedb = await _context.Courses.Include(d => d.CourseDetail).AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);
    //    if (Coursedb == null)
    //    {
    //        return NotFound();
    //    }


    //    _context.Entry(Course).State = EntityState.Modified;
    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int Id, CoursePostVM courseVm)
    {
        //if (Id != courseVm.Id)
        //{
        //    return BadRequest();
        //}
        if (!ModelState.IsValid)
        {
            // Repopulate dropdown data (categories and teachers)
            ViewBag.CourseCategory = await _context.CourseCategories.ToListAsync();
            ViewBag.Teachers = await _context.Teachers.ToListAsync();
            return View(courseVm);
        }

        // Retrieve the existing Course entity
        Course? courseToUpdate = await _context.Courses
            .Include(d => d.CourseDetail)
            .FirstOrDefaultAsync(c => c.Id == Id);

        if (courseToUpdate == null)
        {
            return NotFound();
        }

        try
        {
            // Update the Course entity with data from the view model
            courseToUpdate.Title = courseVm.Title;
            courseToUpdate.Description = courseVm.Description;
            courseToUpdate.ImagePath = courseVm.ImagePath;
            courseToUpdate.CourseCategoryId = courseVm.CourseCatagoryId;
            courseToUpdate.Type = courseVm.Type;
            courseToUpdate.Price = courseVm.Price;
            courseToUpdate.TeachersId = courseVm.TeachersId;

            // Update CourseDetail properties
            courseToUpdate.CourseDetail.VideoPath = courseVm.VideoPath;
            courseToUpdate.CourseDetail.Lessons = courseVm.Lessons;
            courseToUpdate.CourseDetail.Start = courseVm.Start;
            courseToUpdate.CourseDetail.Duration = courseVm.Duration;

            // Set entity state to Modified and save changes
            _context.Entry(courseToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle concurrency conflicts if necessary
            // You can add custom logic here if needed
            throw;
        }
    }

}
