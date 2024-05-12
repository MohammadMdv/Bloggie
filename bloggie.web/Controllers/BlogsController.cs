using bloggie.web.Models.ViewModels;
using bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogPostsRepository _blogPostsRepository;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;

    public BlogsController(IBlogPostsRepository blogPostsRepository,
        IBlogPostLikeRepository blogPostLikeRepository)
    {
        _blogPostsRepository = blogPostsRepository;
        _blogPostLikeRepository = blogPostLikeRepository;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var blogPost = await _blogPostsRepository.GetByUrlHandleAsync(urlHandle);
        var blogDetailsViewModel = new BlogDetailsViewModel();
        if (blogPost != null)
        {
            var likesCount = await _blogPostLikeRepository.GetLikesCount(blogPost.Id);
            blogDetailsViewModel = new BlogDetailsViewModel
            {
                Id = blogPost.Id,
                Heading = blogPost.Heading,
                PageTitle = blogPost.PageTitle,
                Content = blogPost.Content,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                Visible = blogPost.Visible,
                Tags = blogPost.Tags,
                LikesCount = likesCount
            };
        }
        return View(blogDetailsViewModel);
    }
}