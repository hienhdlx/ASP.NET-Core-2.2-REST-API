using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook2.Contracts.V1;
using TweetBook2.Contracts.V1.Requests;
using TweetBook2.Contracts.V1.Respones;
using TweetBook2.Domain;
using TweetBook2.Services;

namespace TweetBook2.Controllers.V1
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _postService.GetPostsAsync());
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest updatePostRequest)
        {
            var post = new Post()
            {
                ID = postId,
                Name = updatePostRequest.Name
            };

            var update = await _postService.UpdatePostAsync(post);
            if (update) return Ok(post);
            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var deletePost = await _postService.DeletePostAsync(postId);
            if (deletePost) return Ok("Delete Successfully");
            return NotFound();
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Name = postRequest.Name };
          //1.read a post
            //1.2 add to post
            await _postService.CreatePostAsync(post);
            // looking for baseUrl
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            //1.3 locationUri
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.ID.ToString());
            return Created(locationUri, post);
        }
    }
}
