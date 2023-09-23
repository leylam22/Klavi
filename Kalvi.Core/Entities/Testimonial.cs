using Kalvi.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace Kalvi.Core.Entities;

public class Testimonial : IEntity
{
    public int Id { get ; set ; }
    [Required, MaxLength(55)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(255)]
    public string Description { get; set; } = null!;
    [Required, MaxLength(55)]
    public string Position { get; set; } = null!;
    [Required, MaxLength(255)]
    public string ImagePath { get; set; } = null!;
}
