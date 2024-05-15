using bloggie.web.Models.ViewModels;
using bloggie.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

[Authorize (Roles = "Admin, SuperAdmin")]
public class AdminUsersController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<IdentityUser> _userManager;

    public AdminUsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var users = await _userRepository.GetAllUsers();
        var model = new UserViewModel
        {
            Users = new List<User>()
        };
        foreach (var user in users) 
        {
            model.Users.Add(new User
            {
                Id = Guid.Parse(user.Id),
                UserName = user.UserName,
                Email = user.Email,
                Role = await _userRepository.GetRole(user)
            });
        }
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> List(UserViewModel model)
    {
        var user = new IdentityUser
        {
            UserName = model.Username,
            Email = model.Email
        };
        var identityResult = await _userManager.CreateAsync(user, model.Password);
        if (identityResult.Succeeded)
        {
            var roles = new List<string> { "User" };
            if (model.IsAdmin)
            {
                roles.Add("Admin");
            }

            identityResult = await _userManager.AddToRolesAsync(user, roles);
            if (identityResult.Succeeded)
            {
                return await List();
            }
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is not null)
        {
            var identityResult = await _userManager.DeleteAsync(user);

            if (identityResult.Succeeded)
            {
                return RedirectToAction("List", "AdminUsers");
            }
        }

        return NotFound();
    }
    
}