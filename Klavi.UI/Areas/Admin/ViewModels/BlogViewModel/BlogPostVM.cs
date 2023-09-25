using System.ComponentModel.DataAnnotations;

namespace Klavi.UI.Areas.Admin.ViewModels.BlogViewModel;

public class BlogPostVM
{
    [Required, MaxLength(255)]
    public string ImagePath { get; set; } = null!;
    [Required, MaxLength(50)]
    public string Blogger { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;
    [Required, MaxLength(250)]
    public string Description { get; set; } = null!;

    [Required, MaxLength(3500)]
    public string BlogInfo { get; set; } = null!;
}
