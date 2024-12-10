using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.Interfaces;
using social_media_app.Models;
using System.Reflection.Metadata.Ecma335;

namespace social_media_app.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IStoryRepository _storyRepository;
        public StoryController(IUserRepository userRepository, IStoryRepository storyRepository)
        {
            _userRepository = userRepository;
            _storyRepository = storyRepository;
        }

        [HttpPost("story")]
        [Authorize]
        public async Task<ActionResult<Story>> CreateNewStory([FromBody] Story story, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                var reqUser = await _userRepository.FindUserByJwt(jwt);
                var createdStory = await _storyRepository.CreateStory(story, reqUser.Id);
                return Ok(createdStory);
            }
            catch (Exception)
            {
                throw new Exception("Create story failed!");
            }
        }

        [HttpGet("story/user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Story>>> GetUsersStories(int userId)
        {
            var stories = await _storyRepository.FindStoriesByUserId(userId);
            return Ok(stories);
        }
    }
}
