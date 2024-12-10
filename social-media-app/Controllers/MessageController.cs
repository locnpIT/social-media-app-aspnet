using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.Interfaces;
using social_media_app.Models;

namespace social_media_app.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        public MessageController(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        [HttpPost("messages/chat/{chatId}")]
        [Authorize]
        public async Task<ActionResult<Message>> CreateNewMessage([FromHeader(Name = "Authorization")] string jwt, [FromBody] Message message, int chatId)
        {
            var user = await _userRepository.FindUserByJwt(jwt);
            var newMessage = await _messageRepository.CreateMessage(user, chatId, message);
            return Ok(newMessage);
        }

        [HttpGet("messages/chat/{chatId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Message>>> FindChatsMessages(int chatId)
        {
            var messages = await _messageRepository.FindChatMessages(chatId);
            return Ok(messages);
        }
    }
}
