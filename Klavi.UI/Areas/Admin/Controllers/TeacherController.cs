using AutoMapper;
using Kalvi.Core.Entities;
using Kalvi.Core.Utilites;
using Kalvi.DataAccess.Contexts;
using Klavi.UI.Areas.Admin.ViewModels.BlogViewModel;
using Klavi.UI.Areas.Admin.ViewModels.TeacherViewModel;
using Klavi.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klavi.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles =UserRole.Admin)]
public class TeacherController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TeacherController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Teachers.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(TeacherPostVm teacherPostVm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Teachers teachers = _mapper.Map<Teachers>(teacherPostVm);
        await _context.Teachers.AddAsync(teachers);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        Teachers? teachers = await _context.Teachers.FindAsync(id);
        if (teachers == null) return NotFound();
        return View(teachers);
    }

    [HttpPost]
    [ActionName("Delete")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Teachers teachers = await _context.Teachers.FindAsync(id);
        if (teachers == null) return NotFound();
        _context.Teachers.Remove(teachers);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        Teachers? teachers = await _context.Teachers.FindAsync(id);
        if (teachers == null) return NotFound();
        return View(teachers);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, Teachers teachers)
    {
        if (id != teachers.Id) return BadRequest();
        if (!ModelState.IsValid) return View(teachers);
        Teachers? teacherdb = await _context.Teachers.AsNoTracking().FirstOrDefaultAsync( t => t.Id == id);
        if (teacherdb == null) return NotFound();
        _context.Entry(teachers).State= EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
