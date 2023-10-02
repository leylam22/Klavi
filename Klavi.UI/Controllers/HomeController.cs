﻿using Kalvi.DataAccess.Contexts;
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

                Courses = await _context.Courses.Include(x => x.CourseCategory).Include(t => t.Teachers).OrderByDescending(c => c.Created).ToListAsync(),
                CourseCategories= await _context.CourseCategories.ToListAsync(),
                //Teachers=await _context.Teachers.ToListAsync(),
                Teachers= await _context.Teachers.ToListAsync()
            };
            return View(vm);
        }

        //public async Task<IActionResult> SearchCourse(CourseFilterVM courseFilterVM)
        //{
        //    var query = _context.Courses.Include(x => x.CourseCategory).OrderByDescending(c => c.Created);
        //    if (!string.IsNullOrEmpty(courseFilterVM.Keywords))
        //    {
        //        query = query.Where(c => c.Title.Contains(courseFilterVM.Keywords));
        //    }
        //    if (courseFilterVM.CoursseCategoryId.HasValu)
        //    {
        //        query = query.Where(c=>c.CourseCategory.Id == courseFilterVM.CoursseCategoryId.Value)
        //    }

        //    HomeVM vm = new HomeVM()
        //    {

        //        Courses = await _context.Courses.Include(x => x.CourseCategory).OrderByDescending(c => c.Created).ToListAsync(),
        //        CourseCategories= await _context.CourseCategories.ToListAsync(),
        //        Teachers=await _context.Teachers.ToListAsync(),
        //    };
        //    return View(vm);
        //}

    }
}
