using Microsoft.EntityFrameworkCore;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.Models;
using System.Runtime.CompilerServices;

namespace social_media_app.Repository
{
    public class ReelsRepository : IReelsRepository
    {
        private readonly SocialMediaContext _context;
        private readonly IUserRepository _userRepository;

        public ReelsRepository(SocialMediaContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<Reels> CreateReels(Reels reels, User user)
        {
            var newReels = new Reels
            {
                title = reels.title,
                user = user,
                video = reels.video
            };
            _context.reels.Add(newReels);
            await _context.SaveChangesAsync();
            return newReels;
        }

        public async Task<IEnumerable<Reels>> FindUserReels(int userId)
        {
            var listReels = await _context.reels
                .Where(r => r.user.Id == userId)
                .Include(r => r.user)
                .ToListAsync();
            return listReels;
        }

        public async Task<IEnumerable<Reels>> GetAllReels()
        {
            return await _context.reels
                .Include(r => r.user)
                .ToListAsync();
        }
    }
}
