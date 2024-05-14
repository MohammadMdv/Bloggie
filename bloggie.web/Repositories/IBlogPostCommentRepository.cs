﻿using bloggie.web.Models.Domain;

namespace bloggie.web.Repositories;

public interface IBlogPostCommentRepository
{
    Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment);
}