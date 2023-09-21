using Kalvi.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace Kalvi.Core.Entities;

public class Teachers : IEntity
{
    public int Id { get ; set ; }
    [Required, MaxLength(100)]
    public string FullName { get; set; } = null!;
    [Required, MaxLength(255)]
    public string ImagePath { get; set; } = null!;
    [Required, MaxLength(100)] 
    public string Position { get; set; } = null!;
}
