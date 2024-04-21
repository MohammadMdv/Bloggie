using bloggie.web.Data;
using bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace bloggie.web.Repositories;

public class TagRepository : ITagRepository
{
    private readonly BloggieDbContext _bloggieDbContext;
    
    public TagRepository(BloggieDbContext bloggieDbContext)
    {
        this._bloggieDbContext = bloggieDbContext;
    }

    public async Task<IEnumerable<Tag?>> GetAllAsync()
    {
        return await _bloggieDbContext.Tags.ToListAsync();
    }

    public async Task<Tag?> GetAsync(Guid id)
    {
        return await _bloggieDbContext.Tags.FindAsync(id);
    }

    public async Task<Tag?> AddAsync(Tag? tag)
    {
        await _bloggieDbContext.Tags.AddAsync(tag);
        await _bloggieDbContext.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> UpdateAsync(Tag tag)
    {
        var existingTag = await _bloggieDbContext.Tags.FindAsync(tag.Id);
        if (existingTag == null)
        {
            return null;
        }

        existingTag.Name = tag.Name;
        existingTag.DisplayName = tag.DisplayName;
        await _bloggieDbContext.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> DeleteAsync(Guid id)
    {
        var tag = await _bloggieDbContext.Tags.FindAsync(id);
        if (tag == null)
        {
            return null;
        }

        _bloggieDbContext.Tags.Remove(tag);
        await _bloggieDbContext.SaveChangesAsync();
        return tag;
    }
}