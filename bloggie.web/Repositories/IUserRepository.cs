using Microsoft.AspNetCore.Identity;

namespace bloggie.web.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<IdentityUser>> GetAllUsers();
    Task<string?> GetRole(IdentityUser user);
}