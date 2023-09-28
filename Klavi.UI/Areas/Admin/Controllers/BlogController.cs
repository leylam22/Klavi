using AutoMapper;
using Kalvi.Core.Entities;
using Kalvi.Core.Utilites;
using Kalvi.DataAccess.Contexts;
using Klavi.UI.Areas.Admin.ViewModels.BlogViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Kalvi.Core.Utilites.UserRole;

namespace Klavi.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = UserRole.Admin)]
public class BlogController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public BlogController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Blogs.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(BlogPostVM blogPostVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Blog blog = _mapper.Map<Blog>(blogPostVM);
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        Blog? blogdb = await _context.Blogs.FindAsync(id);
        if (blogdb == null)
        {
            return NotFound();
        }
        return View(blogdb);
    }

    [HttpPost]
    [ActionName("Delete")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Blog? blogdb = await _context.Blogs.FindAsync(id);
        if (blogdb == null)
        {
            return NotFound();
        }
        _context.Blogs.Remove(blogdb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        Blog? blogdb = await _context.Blogs.FindAsync(id);
        if (blogdb == null)
        {
            return NotFound();
        }
        return View(blogdb);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int Id, Blog blog)
    {
        if (Id != blog.Id)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return View(blog);
        }
        Blog? blogdb = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.Id == Id);
        if (blogdb == null)
        {
            return NotFound();
        }
        _context.Entry(blog).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
