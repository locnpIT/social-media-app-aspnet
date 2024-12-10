using social_media_app.Models;

namespace social_media_app.Interfaces
{
    public interface IReelsRepository
    {
        public Task<Reels> CreateReels(Reels reels, User user);
        public Task<IEnumerable<Reels>> GetAllReels();
        public Task<IEnumerable<Reels>> FindUserReels(int userId);
    }
}
