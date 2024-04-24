using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

public class AdminBlogPostsController : Controller
{
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
}