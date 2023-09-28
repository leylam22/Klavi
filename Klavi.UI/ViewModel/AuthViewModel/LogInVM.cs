using System.ComponentModel.DataAnnotations;

namespace Klavi.UI.ViewModel.AuthViewModel;

public class LogInVM
{
    [Required]
    public string UserNameOrEmail { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    public bool RememberMe { get; set; }
}
