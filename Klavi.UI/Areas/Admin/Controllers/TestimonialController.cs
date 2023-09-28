using AutoMapper;
using Kalvi.Core.Entities;
using Kalvi.Core.Utilites;
using Kalvi.DataAccess.Contexts;
using Klavi.UI.Areas.Admin.ViewModels.TestimonialViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Kalvi.Core.Utilites.UserRole;

namespace Klavi.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = UserRole.Admin)]
public class TestimonialController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TestimonialController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Testimonials.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(TestimonialPostVM TestimonialPostVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Testimonial testimonial = _mapper.Map<Testimonial>(TestimonialPostVM);
        await _context.Testimonials.AddAsync(testimonial);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        Testimonial? Testimonialdb = await _context.Testimonials.FindAsync(id);
        if (Testimonialdb == null)
        {
            return NotFound();
        }
        return View(Testimonialdb);
    }

    [HttpPost]
    [ActionName("Delete")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Testimonial? Testimonialdb = await _context.Testimonials.FindAsync(id);
        if (Testimonialdb == null)
        {
            return NotFound();
        }
        _context.Testimonials.Remove(Testimonialdb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        Testimonial? Testimonialdb = await _context.Testimonials.FindAsync(id);
        if (Testimonialdb == null)
        {
            return NotFound();
        }
        return View(Testimonialdb);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int Id, Testimonial testimonial)
    {
        if (Id != testimonial.Id)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return View(testimonial);
        }
        Testimonial? Testimonialdb = await _context.Testimonials.AsNoTracking().FirstOrDefaultAsync(b => b.Id == Id);
        if (Testimonialdb == null)
        {
            return NotFound();
        }
        _context.Entry(testimonial).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
