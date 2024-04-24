using bloggie.web.Models.ViewModels;
using bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bloggie.web.Controllers;

public class AdminBlogPostsController : Controller
{
    private readonly ITagRepository _tagRepository;

    public AdminBlogPostsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var tags = await _tagRepository.GetAllAsync();
        var model = new AddBlogPostRequest
        {
            Tags = tags.Select(t => new SelectListItem(t?.Name, t?.Id.ToString()))
        };
        return View(model);
    }
}