using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.Interfaces;
using social_media_app.Models;

namespace social_media_app.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ReelsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IReelsRepository _reelsRepository;

        public ReelsController(IUserRepository userRepository, IReelsRepository reelsRepository)
        {
            _userRepository = userRepository;
            _reelsRepository = reelsRepository;
        }

        [HttpPost("reels")]
        [Authorize]
        public async Task<ActionResult<Reels>> CreateNewReels([FromBody] Reels reels, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                var reqUser = await _userRepository.FindUserByJwt(jwt);
                var createdReels = await _reelsRepository.CreateReels(reels, reqUser);
                return Ok(createdReels);
            }
            catch (Exception)
            {
                throw new Exception("Create reels failed!");
            }
        }

        [HttpGet("reels")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reels>>> GetAllReels()
        {
            var listReels = await _reelsRepository.GetAllReels();
            return Ok(listReels);
        }

        [HttpGet("reels/user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reels>>> FindUsersReels(int userId)
        {
            var listReels = await _reelsRepository.FindUserReels(userId);
            return Ok(listReels);
        }
    }
}
