using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bloggie.web.Models;
using bloggie.web.Models.Domain;
using bloggie.web.Models.ViewModels;
using bloggie.web.Repositories;

namespace bloggie.web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogPostsRepository _blogPostsRepository;
    private readonly ITagRepository _tagRepository;

    public HomeController(ILogger<HomeController> logger,
        IBlogPostsRepository blogPostsRepository,
        ITagRepository tagRepository)
    {
        _logger = logger;
        _blogPostsRepository = blogPostsRepository;
        _tagRepository = tagRepository;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<BlogPost> blogPosts = (await _blogPostsRepository.GetAllAsync())!;
        IEnumerable<Tag> tags = (await _tagRepository.GetAllAsync())!;
        var model = new HomeViewModel
        {
            BlogPosts = blogPosts,
            Tags = tags
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Search()
    {
        throw new NotImplementedException();
    }
}