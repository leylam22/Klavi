using Kalvi.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace Kalvi.Core.Entities;

public class Blog : IEntity
{
    public int Id { get ; set ; }
    [Required, MaxLength(255)]
    public string ImagePath { get; set; } = null!;
    [Required, MaxLength(50)]
    public string Blogger { get; set; } = null!;
    public DateTime Date { get; set; }
    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;
    [Required, MaxLength(250)]
    public string Description { get; set; } = null!;
    [Required, MaxLength(3500)]
    public string BlogInfo { get; set; } = null!;
}
