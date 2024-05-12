using bloggie.web.Data;
using bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace bloggie.web.Repositories;

public class BlogPostLikeRepository : IBlogPostLikeRepository
{
    private readonly BloggieDbContext _bloggieDbContext;

    public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
    {
        _bloggieDbContext = bloggieDbContext;
    }
    public async Task<int> GetLikesCount(Guid blogPostId)
    {
        return await _bloggieDbContext.BlogPostLikes.CountAsync(x => x.BlogPostId == blogPostId);
    }

    public async Task<BlogPostLike> AddLike(BlogPostLike blogPostLike)
    {
        await _bloggieDbContext.BlogPostLikes.AddAsync(blogPostLike);
        await _bloggieDbContext.SaveChangesAsync();
        return blogPostLike;
    }
}