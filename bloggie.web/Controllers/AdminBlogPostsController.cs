using bloggie.web.Models.Domain;
using bloggie.web.Models.ViewModels;
using bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bloggie.web.Controllers;

public class AdminBlogPostsController : Controller
{
    private readonly ITagRepository _tagRepository;
    private readonly IBlogPostsRepository _blogPostsRepository;

    public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostsRepository blogPostsRepository)
    {
        _tagRepository = tagRepository;
        _blogPostsRepository = blogPostsRepository;
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
        var blogPost = new BlogPost
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
        var tags = new List<Tag>();
        foreach (var tagId in addBlogPostRequest.SelectedTags)
        {
            var existingTag = await _tagRepository.GetAsync(Guid.Parse(tagId));
            if (existingTag != null) tags.Add(existingTag);
        }
        blogPost.Tags = tags;
        await _blogPostsRepository.AddAsync(blogPost);
        return RedirectToAction("Add");
    }
}