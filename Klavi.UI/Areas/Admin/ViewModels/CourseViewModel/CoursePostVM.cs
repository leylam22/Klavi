using Kalvi.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Klavi.UI.Areas.Admin.ViewModels.CourseViewModel;

public class CoursePostVM
{
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
    [Required, MaxLength(255)]
    public string VideoPath { get; set; } = null!;
    public int Lessons { get; set; }
    [Required, MaxLength(50)]
    public string Duration { get; set; } = null!;
    [Required]
    public DateTime Start { get; set; }
    public int CourseCatagoryId { get; set; }
}
