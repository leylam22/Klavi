using Kalvi.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace Kalvi.Core.Entities;

public class Teacher : IEntity
{
    public int Id { get ; set ; }
    [Required, MaxLength(100)]
    public string FullName { get; set; } = null!;
    [Required, MaxLength(255)]
    public string ImagePath { get; set; } = null!;
    [Required, MaxLength(100)]
    public string Position { get; set; } = null!;
    public ICollection<Course> Courses { get; set; }
}
