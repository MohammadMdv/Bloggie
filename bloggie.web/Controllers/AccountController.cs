using bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    // GET
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(registerViewModel);
        }

        var user = new IdentityUser
        {
            UserName = registerViewModel.Username,
            Email = registerViewModel.Email
        };

        var identityResult = await _userManager.CreateAsync(user, registerViewModel.Password);

        if (identityResult.Succeeded)
        {
            var roleIdentityResult = await _userManager.AddToRoleAsync(user, "User");
            if (roleIdentityResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        return View(); 
    }
}