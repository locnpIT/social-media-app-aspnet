using Microsoft.EntityFrameworkCore;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private readonly SocialMediaContext _context;
        private readonly IUserRepository _userRepository;
        public StoryRepository(SocialMediaContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<Story> CreateStory(Story story, int userId)
        {
            var reqUser = await _userRepository.FindUserById(userId);
            var newStory = new Story
            {
                caption = story.caption,
                image = story.image,
                timestamp = DateTime.Now,
                user = reqUser
            };
            _context.stories.Add(newStory);
            await _context.SaveChangesAsync();
            return newStory;
        }

        public async Task<IEnumerable<Story>> FindStoriesByUserId(int userId)
        {
            var stories = await _context.stories
                .Where(s => s.user.Id == userId)
                .Include(s => s.user)
                .ToListAsync();
            return stories;
        }
    }
}
