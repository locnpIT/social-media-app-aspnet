using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using social_media_app.DTOs.Chat;
using social_media_app.Interfaces;
using social_media_app.IRepository;
using social_media_app.Models;

namespace social_media_app.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public ChatController(IUserRepository userRepository, IChatRepository chatRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
        }

        [HttpPost("chats")]
        [Authorize]
        public async Task<ActionResult<Chat>> CreateNewChat([FromHeader(Name = "Authorization")] string jwt, [FromBody] ChatDTO chatDTO)
        {
            try
            {
                var reqUser = await _userRepository.FindUserByJwt(jwt);
                var user2 = await _userRepository.FindUserById(chatDTO.userId);
                var chat = await _chatRepository.CreateChat(reqUser, user2);
                return Ok(chat);
            }
            catch (Exception)
            {
                throw new Exception("Create chat failed!");
            }
        }

        [HttpGet("chats")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Chat>>> FindUsersChat([FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                var user = await _userRepository.FindUserByJwt(jwt);
                var chats = await _chatRepository.FindUsersChat(user.Id);
                return Ok(chats);
            }
            catch (Exception)
            {
                throw new Exception("Chat not found!");
            }
        }
    }
}
