using Kalvi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kalvi.DataAccess.Contexts;

public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Blog> Blogs { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<CourseCategory> CourseCategories { get; set; }
	public DbSet<CourseDetail> CourseDetails { get; set; }
	public DbSet<Teachers> Teachers { get; set; }
}
