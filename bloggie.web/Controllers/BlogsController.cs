using bloggie.web.Models.Domain;
using bloggie.web.Models.ViewModels;
using bloggie.web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogPostsRepository _blogPostsRepository;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IBlogPostCommentRepository _blogPostCommentRepository;

    public BlogsController(IBlogPostsRepository blogPostsRepository,
        IBlogPostLikeRepository blogPostLikeRepository,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IBlogPostCommentRepository blogPostCommentRepository)
    {
        _blogPostsRepository = blogPostsRepository;
        _blogPostLikeRepository = blogPostLikeRepository;
        _signInManager = signInManager;
        _userManager = userManager;
        _blogPostCommentRepository = blogPostCommentRepository;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var liked = false;
        var blogPost = await _blogPostsRepository.GetByUrlHandleAsync(urlHandle);
        var blogDetailsViewModel = new BlogDetailsViewModel();
        if (blogPost != null)
        {
            var likesCount = await _blogPostLikeRepository.GetLikesCount(blogPost.Id);
            if (_signInManager.IsSignedIn(User))
            {
                var likesForBlog = await _blogPostLikeRepository.GetLikesForBlog(blogPost.Id);
                var userId = _userManager.GetUserId(User);
                if (userId != null)
                {
                    var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                    liked = likeFromUser != null;
                }
            }
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
                LikesCount = likesCount,
                Liked = liked
            };
        }
        return View(blogDetailsViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
    {
        if (_signInManager.IsSignedIn(User))
        {
            var domainModel = new BlogPostComment
            {
                BlogPostId = blogDetailsViewModel.Id,
                Description = blogDetailsViewModel.Comment,
                UserId = Guid.Parse(_userManager.GetUserId(User) ?? string.Empty),
                DateAdded = DateTime.Now
            };
            await _blogPostCommentRepository.AddCommentAsync(domainModel);
            
            return RedirectToAction("Index", "Home", 
                new { blogDetailsViewModel.UrlHandle});
        }

        return View();
    }
}