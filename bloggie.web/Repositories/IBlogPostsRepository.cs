﻿using bloggie.web.Models.Domain;

namespace bloggie.web.Repositories;

public interface IBlogPostsRepository
{
    public Task<IEnumerable<BlogPost?>> GetAllAsync();
    public Task<BlogPost?> GetAsync(Guid id);
    public Task<BlogPost> AddAsync(BlogPost blogPost);
    public Task<BlogPost?> UpdateAsync(BlogPost blogPost);
    public Task<BlogPost?> DeleteAsync(Guid id);
}