using System.ComponentModel.DataAnnotations;

namespace Klavi.UI.ViewModel.AuthViewModel;

public class RegisterVM
{
    [Required]
    public string Fullname { get; set; } = null!;
    [Required]
    public string Username { get; set; } = null!;
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string ComfirmPassword { get; set; } = null!;
}
