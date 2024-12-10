
using social_media_app.Models;

namespace social_media_app.Interfaces
{
    public interface IMessageRepository
    {
        public Task<Message> CreateMessage(User user, int chatId, Message req);
        public Task<IEnumerable<Message>> FindChatMessages(int chatId);
        public Task<IEnumerable<Message>> FindMessageByChatId(int chatId);
    }
}
