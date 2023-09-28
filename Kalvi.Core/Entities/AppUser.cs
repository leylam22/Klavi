using Microsoft.AspNetCore.Identity;

namespace Kalvi.Core.Entities;

public class AppUser: IdentityUser
{
    public string? Fullname { get; set; }
}
