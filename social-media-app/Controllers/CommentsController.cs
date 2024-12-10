using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.Models;
using System.Net;

namespace social_media_app.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly SocialMediaContext _context;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentsController(SocialMediaContext context, ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _context = context;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        [HttpGet("comments/{id}")]
        [Authorize]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _commentRepository.FindCommentById(id);
            return Ok(comment);
        }

        [HttpPost("comments/post/{postId}")]
        [Authorize]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment comment, int postId, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                var reqUser = await _userRepository.FindUserByJwt(jwt);

                var createComment = await _commentRepository.CreateComment(comment, postId, reqUser.Id);

                return Ok(createComment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }

        [HttpPut("comments/like/{commentId}")]
        [Authorize]
        public async Task<ActionResult<Comment>> LikeComment(int commentId, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                var reqUser = await _userRepository.FindUserByJwt(jwt);
                var likeComment = await _commentRepository.LikeComment(commentId, reqUser.Id);

                return Ok(likeComment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }
    }
}
