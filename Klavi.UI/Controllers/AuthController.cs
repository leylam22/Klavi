using Kalvi.Core.Entities;
using Kalvi.Core.Utilites;
using Klavi.UI.ViewModel.AuthViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Klavi.UI.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Register(RegisterVM newUser)
    {
        if (!ModelState.IsValid)
        {
            return View(newUser);
        }
        AppUser user = new()
        {
            Fullname = newUser.Username,
            Email = newUser.Email,
            UserName = newUser.Username
        };
        IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);
        if (result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }
        await _userManager.AddToRoleAsync(user, UserRole.Admin);
        return RedirectToAction(nameof(LogIn));
    }

    public IActionResult LogIn() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogIn(LogInVM logIn)
    {
        if (!ModelState.IsValid) return View(logIn);
        AppUser user = await _userManager.FindByEmailAsync(logIn.UserNameOrEmail);
        if (user is null)
        {
            ModelState.AddModelError("", "Invalid login");
            return View(logIn);
        }
        Microsoft.AspNetCore.Identity.SignInResult result = await
        _signInManager.PasswordSignInAsync(user, logIn.Password, logIn.RememberMe, true);
        if (result.IsLockedOut)
        {
            ModelState.AddModelError("", "Your account is locked. Try it later");
            return View(logIn);
        }
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Invalid login");
            return View(logIn);
        }

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        if (User.Identity.IsAuthenticated)
        {
            await _signInManager.SignOutAsync();
        }
        return RedirectToAction("Index", "Home");
    }

    #region Create Role
    //[AllowAnonymous]
    //public async Task CreateRole()
    //{
    //    foreach (var role in Enum.GetValues(typeof(UserRole.Roles)))
    //    {
    //        bool isExsist = await _roleManager.RoleExistsAsync(role.ToString());
    //        if (!isExsist)
    //        {
    //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
    //        }
    //    }
    //}
    #endregion

    public IActionResult ForgotPassword() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
    {
        if (!ModelState.IsValid)
            return View(forgotPasswordModel);
        var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
        if (user == null)
            return RedirectToAction(nameof(ForgotPasswordConfirmation));

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callback = Url.Action(nameof(ResetPassword), "Auth", new { token, email = user.Email }, Request.Scheme);
        return Content(callback);
        //var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
        //await _emailSender.SendEmailAsync(message);
    }

    public IActionResult ForgotPasswordConfirmation() => View();

    public IActionResult ResetPassword(string token, string email)
    {
        var model = new ResetPasswordModel { Token = token, Email = email };
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
    {
        if (!ModelState.IsValid) return View(resetPasswordModel);
        AppUser user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
        if (user == null) RedirectToAction(nameof(ResetPasswordConfirmation));
        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
        if (!resetPassResult.Succeeded)
        {
            foreach (var error in resetPassResult.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return View();
        }
        else
        {

        return RedirectToAction(nameof(ResetPasswordConfirmation));
        }
    }
    [HttpGet]
    public IActionResult ResetPasswordConfirmation() => View();
}
