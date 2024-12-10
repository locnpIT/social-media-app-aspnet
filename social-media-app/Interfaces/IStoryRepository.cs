using social_media_app.Models;

namespace social_media_app.Interfaces
{
    public interface IStoryRepository
    {
        public Task<Story> CreateStory(Story story, int userId);
        public Task<IEnumerable<Story>> FindStoriesByUserId(int userId);
    }
}
