using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.IRepository;
using social_media_app.Models;
using System.Runtime.CompilerServices;

namespace social_media_app.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IChatRepository _chatRepository;
        private readonly SocialMediaContext _context;
        public MessageRepository(IChatRepository chatRepository, SocialMediaContext context)
        {
            _chatRepository = chatRepository;
            _context = context;
        }

        public async Task<Message> CreateMessage(User user, int chatId, Message req)
        {
            var chat = await _chatRepository.FindChatById(chatId);
            var message = new Message
            {
                chat = chat,
                content = req.content,
                image = req.image,
                user = user,
                timestamp = DateTime.Now
            };
            _context.messages.Add(message);
            chat.messages.Add(message);

            await _context.SaveChangesAsync();

            return message;
        }



        public async Task<IEnumerable<Message>> FindChatMessages(int chatId)
        {
            var chat = await _chatRepository.FindChatById(chatId);
            if (chat == null)
            {
                throw new Exception("Can't find this chat!");
            }
            var messages = await FindMessageByChatId(chatId);
            return messages;
        }

        public async Task<IEnumerable<Message>> FindMessageByChatId(int chatId)
        {
            var messages = await _context.messages
                .Where(m => m.chat.Id == chatId)
                .Include(m => m.chat)
                .Include(u => u.user)
                .ToListAsync();
            return messages;
        }
    }
}
