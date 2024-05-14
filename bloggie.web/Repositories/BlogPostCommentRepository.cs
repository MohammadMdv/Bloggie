using bloggie.web.Data;
using bloggie.web.Models.Domain;

namespace bloggie.web.Repositories;

public class BlogPostCommentRepository : IBlogPostCommentRepository
{
    private readonly BloggieDbContext _bloggieDbContext;

    public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
    {
        _bloggieDbContext = bloggieDbContext;
    }
    
    public async Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment)
    {
        await _bloggieDbContext.BlogPostComments.AddAsync(blogPostComment);
        await _bloggieDbContext.SaveChangesAsync();
        return blogPostComment;
    }
}