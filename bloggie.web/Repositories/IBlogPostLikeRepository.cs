namespace bloggie.web.Repositories;

public interface IBlogPostLikeRepository
{
    Task<int> GetLikesCount(Guid blogPostId);
}