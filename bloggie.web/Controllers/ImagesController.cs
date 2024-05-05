using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using bloggie.web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await _imageRepository.UploadAsync(file);
            if (imageUrl == null)
            {
                return Problem("Something went wrong while uploading the image.", null, 
                    (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageUrl });
        }
    }
}
