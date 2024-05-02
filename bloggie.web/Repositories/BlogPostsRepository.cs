using bloggie.web.Data;
using bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace bloggie.web.Repositories;

public class BlogPostsRepository : IBlogPostsRepository
{
    private readonly BloggieDbContext _bloggieDbContext;

    public BlogPostsRepository(BloggieDbContext bloggieDbContext)
    {
        _bloggieDbContext = bloggieDbContext;
    }
    
    public async Task<IEnumerable<BlogPost?>> GetAllAsync()
    {
        return await _bloggieDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
    }

    public async Task<BlogPost?> GetAsync(Guid id)
    {
        return await _bloggieDbContext.BlogPosts.Include(x => x.Tags).
            FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        await _bloggieDbContext.BlogPosts.AddAsync(blogPost);
        await _bloggieDbContext.SaveChangesAsync();
        return blogPost;
    }

    public Task<BlogPost> UpdateAsync(BlogPost blogPost)
    {
        throw new NotImplementedException();
    }

    public Task<BlogPost> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}