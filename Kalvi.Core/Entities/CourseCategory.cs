using Kalvi.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace Kalvi.Core.Entities;

public class CourseCategory : IEntity
{
    public int Id { get ; set ; }
    [Required, MaxLength(255)]
    public string ImagePath { get; set; } = null!;
    [Required, MaxLength(30)]
    public string Catagory { get; set; } = null!;
    public ICollection<Course>? Courses { get; set; }
}
