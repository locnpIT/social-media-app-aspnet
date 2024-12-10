using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.Models;
using social_media_app.Response;
using System.Net;
using System.Runtime.CompilerServices;

namespace social_media_app.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly SocialMediaContext _context;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostsController(SocialMediaContext context, IPostRepository postRepository, IUserRepository userRepository)
        {
            _context = context;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpPost("posts/users")]
        [Authorize]
        public async Task<ActionResult<Post>> CreatNewPost([FromBody] Post post, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                var reqUser = await _userRepository.FindUserByJwt(jwt);
                var newPost = await _postRepository.CreatePost(post, reqUser.Id);
                return Ok(newPost);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("posts")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            return await _postRepository.GetAllPosts();
        }

        [HttpDelete("posts/{postId}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> DeletePost(int postId, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                var reqUser = await _userRepository.FindUserByJwt(jwt);

                var message = await _postRepository.DeletePost(postId, reqUser.Id);

                ApiResponse apiResponse = new ApiResponse 
                {
                    message = message,
                    status = true
                };
                return Ok(apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, null);
            }
        }

        [HttpGet("posts/{postId}")]
        [Authorize]
        public async Task<ActionResult<Post>> FindPostByIdHandler(int postId)
        {
            var post = await _postRepository.FindPostById(postId);
            return Ok(post);
        }

        [HttpGet("posts/user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Post>>> FindUsersPost(int userId)
        {
            var posts = await _postRepository.FindPostsByUserId(userId);
            return Ok(posts);
        }


        [HttpPut("save/posts/{postId}")]
        [Authorize]
        public async Task<ActionResult<Post>> SavedPostHandler(int postId, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                var reqUser = await _userRepository.FindUserByJwt(jwt);
                var post = await _postRepository.SavePost(postId, reqUser.Id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't save post " + ex.Message);
            }
        }

        [HttpPut("posts/like/{postId}")]
        [Authorize]
        public async Task<ActionResult<Post>> LikedPostHandler(int postId, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                var reqUser = await _userRepository.FindUserByJwt(jwt);
                var post = await _postRepository.LikePost(postId, reqUser.Id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't like post " + ex.Message);
            }
        }
    }
}
