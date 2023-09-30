using System.ComponentModel.DataAnnotations;

namespace Klavi.UI.Areas.Admin.ViewModels.TeacherViewModel;

public class TeacherPostVm
{
    [Required, MaxLength(255)]
    public string ImagePath { get; set; }

    [Required, MaxLength(55)]
    public string FullName { get; set; } = null!;
    [Required, MaxLength(50)]
    public string Position { get; set; } = null!;
}
