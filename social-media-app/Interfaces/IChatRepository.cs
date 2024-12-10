using social_media_app.Models;

namespace social_media_app.IRepository
{
    public interface IChatRepository
    {
        public Task<Chat> CreateChat(User user, User user2);
        public Task<Chat> FindChatById(int chatId);
        public Task<IEnumerable<Chat>> FindUsersChat(int userId);
        public Task<Chat> FindChatByUsers(User user, User reqUser);
    }
}
