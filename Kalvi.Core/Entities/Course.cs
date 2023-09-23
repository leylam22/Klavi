using Kalvi.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalvi.Core.Entities;

public class Course : IEntity
{
    public int Id { get ; set ; }
    [Required, MaxLength(30)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(120)]
    public string Description { get; set; } = null!;

    [Required, MaxLength(255)]
    public string ImagePath { get; set; } = null!;
    public int Price { get; set; }
    [Required, MaxLength(30)]
    public string Type { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public int CourseCatagoryId { get; set; }
    public CourseCategory? CourseCategory { get; set; }
    //public int CourseDetailId { get; set; }
    public CourseDetail CourseDetail { get; set; }
}
