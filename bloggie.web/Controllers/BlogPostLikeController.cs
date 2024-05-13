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
        
        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemoveLike([FromBody] AddLikeRequest request)
        {
            var blogPostLike = new BlogPostLike
            {
                BlogPostId = request.BlogPostId,
                UserId = request.UserId
            };
            await _blogPostLikeRepository.RemoveLike(blogPostLike);
            return Ok();
        }
        
        [HttpGet]
        [Route("{blogPostId}/LikesCount")]
        public async Task<IActionResult> GetLikesCount([FromRoute] Guid blogPostId)
        {
            var likesCount = await _blogPostLikeRepository.GetLikesCount(blogPostId);
            return Ok(likesCount);
        }
    }
}
