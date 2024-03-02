using bloggie.web.Models.Domain;

namespace bloggie.web.Data;
using Microsoft.EntityFrameworkCore;
public class BloggieDbContext : DbContext
{
    public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Tag> Tags { get; set; }
}