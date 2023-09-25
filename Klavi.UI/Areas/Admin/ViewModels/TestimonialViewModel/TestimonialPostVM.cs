using System.ComponentModel.DataAnnotations;

namespace Klavi.UI.Areas.Admin.ViewModels.TestimonialViewModel;

public class TestimonialPostVM
{
    [Required, MaxLength(55)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(255)]
    public string Description { get; set; } = null!;
    [Required, MaxLength(55)]
    public string Position { get; set; } = null!;
    [Required, MaxLength(255)]
    public string ImagePath { get; set; } = null!;
}
