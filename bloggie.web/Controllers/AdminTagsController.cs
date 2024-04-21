using bloggie.web.Models.Domain;
using bloggie.web.Models.ViewModels;
using bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers;

public class AdminTagsController : Controller
{
    private readonly ITagRepository _tagRepository;

    public AdminTagsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    [HttpGet]
    public IActionResult Add()
    {
        TempData["SuccessMessage"] = null;
        return View();
    }
    
    [HttpPost]
    [ActionName("Add")]
    public async Task<IActionResult> SubmitTag(AddTagRequest addTagRequest)
    {
        var tag = new Tag
        {
            Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName
        };

        await _tagRepository.AddAsync(tag);
        TempData["SuccessMessage"] = "You have created a tag";

        return RedirectToAction("Add");
    }
    
    // Function to list the tags
    [HttpGet]
    [ActionName("List")]
    public async Task<IActionResult> List()
    {
        var tags = await _tagRepository.GetAllAsync();
        return View(tags as List<Tag>);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var tag = await _tagRepository.GetAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        var editTagRequest = new EditTagRequest
        {
            Id = tag.Id,
            Name = tag.Name,
            DisplayName = tag.DisplayName
        };

        return View(editTagRequest);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
    {
        var tag = new Tag
        {
            Id = editTagRequest.Id,
            Name = editTagRequest.Name,
            DisplayName = editTagRequest.DisplayName
        };
        if (await _tagRepository.UpdateAsync(tag) == null)
        {
            return NotFound();
        }

        return RedirectToAction("List");
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tag = await _tagRepository.GetAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        var deleteTagRequest = new DeleteTagRequest
        {
            Id = tag.Id,
            Name = tag.Name,
            DisplayName = tag.DisplayName
        };

        return View(deleteTagRequest);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (await _tagRepository.DeleteAsync(id) == null)
        {
            return NotFound();
        }

        return RedirectToAction("List");
    }
}