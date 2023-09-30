using System.ComponentModel.DataAnnotations;

namespace Klavi.UI.ViewModel.AuthViewModel;

public class ForgotPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
