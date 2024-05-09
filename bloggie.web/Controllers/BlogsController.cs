using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

public class BlogsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}