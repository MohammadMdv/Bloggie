using bloggie.web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bloggie.web.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _authDbContext;

    public UserRepository(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }
    
    public async Task<IEnumerable<IdentityUser>> GetAllUsers()
    {
        var users = await _authDbContext.Users.ToListAsync();
        var superAdminUser = await _authDbContext.Users.FirstOrDefaultAsync(u => u
            .UserName == "superadmin.Mahdavi");
        if (superAdminUser != null)
        {
            users.Remove(superAdminUser);
        }

        return users;
    }

    public async Task<string?> GetRole(IdentityUser user)
    {
        // retrieving the name of the role of the user
        var roles = await _authDbContext.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Join(_authDbContext.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
            .ToListAsync();

        // Define the hierarchy for the roles
        var roleHierarchy = new List<string> { "User", "Admin", "SuperAdmin" };

        // Sort the roles based on the hierarchy and return the highest one
        var highestRole = roles
            .OrderByDescending(r => roleHierarchy.IndexOf(r))
            .FirstOrDefault();

        return highestRole;
    }
}