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

    public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        var existingBlogPost = await GetAsync(blogPost.Id);
        if (existingBlogPost == null)
            return null;
        existingBlogPost.Id = blogPost.Id;
        existingBlogPost.Heading = blogPost.Heading;
        existingBlogPost.PageTitle = blogPost.PageTitle;
        existingBlogPost.Content = blogPost.Content;
        existingBlogPost.ShortDescription = blogPost.ShortDescription;
        existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
        existingBlogPost.UrlHandle = blogPost.UrlHandle;
        existingBlogPost.PublishedDate = blogPost.PublishedDate;
        existingBlogPost.Author = blogPost.Author;
        existingBlogPost.Visible = blogPost.Visible;
        existingBlogPost.Tags = blogPost.Tags;
        await _bloggieDbContext.SaveChangesAsync();
        return existingBlogPost;
    }

    public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
    {
        return await _bloggieDbContext.BlogPosts.Include(x=> x!.Tags).
            FirstOrDefaultAsync(x => x != null && x.UrlHandle == urlHandle);
    }

    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
        var blogPost = await GetAsync(id);
        if (blogPost == null)
            return null;
        _bloggieDbContext.BlogPosts.Remove(blogPost);
        await _bloggieDbContext.SaveChangesAsync();
        return blogPost;
    }
}