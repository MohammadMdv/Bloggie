using bloggie.web.Models.Domain;
using bloggie.web.Models.ViewModels;
using bloggie.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bloggie.web.Controllers;

[Authorize(Roles = "Admin")]
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

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var blogPosts = await _blogPostsRepository.GetAllAsync();
        return View(blogPosts);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var blogPost = await _blogPostsRepository.GetAsync(id);
        var tags = await _tagRepository.GetAllAsync();
        if (blogPost != null)
        {
            var model = new EditBlogPostRequest
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
                Tags = tags.Select(t => new SelectListItem(t?.Name, t?.Id.ToString())),
                SelectedTags = blogPost.Tags?.Select(t => t.Id.ToString()).ToArray() ?? Array.Empty<string>()
            };
            return View(model);
        }
        return View(null);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
    {
        var blogPostDomain = new BlogPost
        {
            Id = editBlogPostRequest.Id,
            Heading = editBlogPostRequest.Heading,
            PageTitle = editBlogPostRequest.PageTitle,
            Content = editBlogPostRequest.Content,
            ShortDescription = editBlogPostRequest.ShortDescription,
            FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
            UrlHandle = editBlogPostRequest.UrlHandle,
            PublishedDate = editBlogPostRequest.PublishedDate,
            Author = editBlogPostRequest.Author,
            Visible = editBlogPostRequest.Visible,
        };
        var tags = new List<Tag>();
        foreach (var tagId in editBlogPostRequest.SelectedTags)
        {
            var existingTag = await _tagRepository.GetAsync(Guid.Parse(tagId));
            if (existingTag != null) tags.Add(existingTag);
        }
        blogPostDomain.Tags = tags;
        if(await _blogPostsRepository.UpdateAsync(blogPostDomain) != null)
            return RedirectToAction("List");
        return View(null);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (await _blogPostsRepository.DeleteAsync(id) != null)
            return Ok();
        return View(null);
    }
}