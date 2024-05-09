using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bloggie.web.Models;
using bloggie.web.Models.Domain;
using bloggie.web.Repositories;

namespace bloggie.web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogPostsRepository _blogPostsRepository;

    public HomeController(ILogger<HomeController> logger, IBlogPostsRepository blogPostsRepository)
    {
        _logger = logger;
        _blogPostsRepository = blogPostsRepository;
    }

    public async Task<IActionResult> Index()
    {
        var blogPosts = await _blogPostsRepository.GetAllAsync();
        return View((List<BlogPost>)blogPosts);
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