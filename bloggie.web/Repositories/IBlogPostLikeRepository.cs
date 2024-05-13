using bloggie.web.Models.Domain;

namespace bloggie.web.Repositories;

public interface IBlogPostLikeRepository
{
    Task<int> GetLikesCount(Guid blogPostId);
    Task<BlogPostLike> AddLike(BlogPostLike blogPostLike);
    Task<BlogPostLike> RemoveLike(BlogPostLike blogPostLike);
}