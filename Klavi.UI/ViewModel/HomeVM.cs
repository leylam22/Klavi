using Kalvi.Core.Entities;

namespace Klavi.UI.ViewModel;

public class HomeVM
{
    public IEnumerable<Course> Courses { get; set; }
    public IEnumerable<CourseCategory> CourseCategories { get; set; }
    public IEnumerable<CourseDetail> CourseDetails { get; set; }
    public IEnumerable<Teachers> Teachers { get; set; }
    public IEnumerable<Blog> Blogs { get; set; }
}
