using bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginViewModel);
        }

        var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

        if (signInResult.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult Logout()
    {
        throw new NotImplementedException();
    }
}