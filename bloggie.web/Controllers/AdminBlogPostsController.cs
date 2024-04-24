using bloggie.web.Models.Domain;
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
    
    [HttpPost]
    public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(addBlogPostRequest);
        }

        var BlogPost = new BlogPost
        {
            Heading = addBlogPostRequest.Heading,
            PageTitle = addBlogPostRequest.PageTitle,
            Content = addBlogPostRequest.Content,
            ShortDescription = addBlogPostRequest.ShortDescription,
            FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
            UrlHandle = addBlogPostRequest.UrlHandle,
            PublishedDate = addBlogPostRequest.PublishedDate,
            Author = addBlogPostRequest.Author,
            Visible = addBlogPostRequest.Visible,
        };
        Tag[] tags = Array.Empty<Tag>();
        foreach (var tagId in addBlogPostRequest.Tags)
        {
            
        }

        return View();
    }
}