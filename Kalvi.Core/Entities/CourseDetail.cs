using Kalvi.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalvi.Core.Entities;

public class CourseDetail : IEntity
{
    public int Id { get ; set ; }
    [Required, MaxLength(255)]
    public string VideoPath { get; set; } = null!;
    public int Lessons { get; set; }
    [Required, MaxLength(50)]
    public string Duration { get; set; } = null!;
    [Required]
    public DateTime Start { get; set; }
    public int CourseId { get; set; }
    [ForeignKey("CourseId")]
    public Course Courses { get; set; }
}
