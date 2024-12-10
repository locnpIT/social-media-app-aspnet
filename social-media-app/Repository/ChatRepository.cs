using Microsoft.EntityFrameworkCore;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.IRepository;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly SocialMediaContext _context;
        public ChatRepository(IUserRepository userRepository, SocialMediaContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<Chat> CreateChat(User reqUser, User user2)
        {
            var isExist = await FindChatByUsers(user2, reqUser);
            if (isExist != null)
            {
                return isExist;
            }
            var chat = new Chat();
            chat.users.Add(user2);
            chat.users.Add(reqUser);
            chat.timestamp = DateTime.Now;

            _context.chats.Add(chat);
            await _context.SaveChangesAsync();

            return chat;
        }

        public async Task<Chat> FindChatById(int chatId)
        {
            var chat = await _context.chats.FindAsync(chatId);
            if (chat == null)
            {
                throw new Exception("Chat not found with id = " + chatId);
            }
            return chat;
        }

        public async Task<IEnumerable<Chat>> FindUsersChat(int userId)
        {
            var user = await _userRepository.FindUserById(userId);
            var chats = await _context.chats
                .Where(c => c.users.Contains(user))
                .Include(c => c.users)
                .Include(c => c.messages)
                .ToListAsync();
            return chats;
        }

        public async Task<Chat> FindChatByUsers(User user, User reqUser)
        {
            var chat = await _context.chats
                .Where(c => c.users.Contains(user) && c.users.Contains(reqUser))
                .Include(c => c.users)
                .Include(c => c.users)
                .FirstOrDefaultAsync();
            return chat;
        }
    }
}
