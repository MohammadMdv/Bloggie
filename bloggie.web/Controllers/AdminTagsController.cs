using bloggie.web.Data;
using bloggie.web.Models.Domain;
using bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

public class AdminTagsController : Controller
{
    private BloggieDbContext _bloggieDbContext;
    public AdminTagsController(BloggieDbContext bloggieDbContext)
    {
        _bloggieDbContext = bloggieDbContext;
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    [ActionName("Add")]
    public IActionResult SubmitTag(AddTagRequest addTagRequest)
    {
        var tag = new Tag
        {
            Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName
        };
        _bloggieDbContext.Tags.Add(tag);
        _bloggieDbContext.SaveChanges();

        TempData["SuccessMessage"] = "You have created a tag";

        return RedirectToAction("Add");
    }
    
    // Function to list the tags
    public IActionResult List()
    {
        var tags = _bloggieDbContext.Tags.ToList();
        return View(tags);
    }
}