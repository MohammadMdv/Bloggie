using bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogPostsRepository _blogPostsRepository;

    public BlogsController(IBlogPostsRepository blogPostsRepository)
    {
        _blogPostsRepository = blogPostsRepository;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var blogPost = await _blogPostsRepository.GetByUrlHandleAsync(urlHandle);
        return View(blogPost);
    }
}