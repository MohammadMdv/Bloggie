using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bloggie.web.Models.ViewModels;

public class RegisterViewModel
{
    [Microsoft.Build.Framework.Required]
    public string Username { get; set; }
    [Microsoft.Build.Framework.Required]
    [EmailAddress]
    public string Email { get; set; }
    [Microsoft.Build.Framework.Required]
    [MinLength(6)]
    public string Password { get; set; }
}