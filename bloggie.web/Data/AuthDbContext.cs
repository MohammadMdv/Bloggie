using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bloggie.web.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // seeding roles

        var userRoleId = "045def90-75f6-48a1-a79b-6cc596ef99b0";
        var adminRoleId = "cb522e47-901b-4157-ba5d-0ff2d7f234d4";
        var superAdminRoleId = "80a3da7b-f252-4af2-b694-c5963b20f332";
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "User",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId
            },
            new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);
        
        // seeding super admin user
        var superAdminId = "c2df059e-775b-437c-8a7a-d9a34e111b90";
        var superAdminUser = new IdentityUser
        {
            Id = superAdminId,
            UserName = "superadmin.Mahdavi",
            NormalizedUserName = "SUPERADMIN.MAHDAVI",
            Email = "mohammad.s13811381@gmail.com",
            NormalizedEmail = "mohammad.s13811381@gmail.com".ToUpper()
        };
    }
}