using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bloggie.web.Models.Domain;
using bloggie.web.Models.ViewModels;
using bloggie.web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
        }
        
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest request)
        {
            var blogPostLike = new BlogPostLike
            {
                BlogPostId = request.BlogPostId,
                UserId = request.UserId
            };
            await _blogPostLikeRepository.AddLike(blogPostLike);
            return Ok();
        }
    }
}
