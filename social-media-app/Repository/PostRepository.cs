using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;
        private readonly IUserRepository _userRepository;
        public PostRepository(SocialMediaContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<Post> CreatePost(Post post, int userId)
        {
            var user = await _userRepository.FindUserById(userId);

            var newPost = new Post
            {
                caption = post.caption,
                image = post.image,
                createAt = DateTime.Now,
                video = post.video,
                user = user
            };

            _context.posts.Add(newPost);
            await _context.SaveChangesAsync();

            return newPost;
        }

        public async Task DeleteFromUsersLikedPost(int postId)
        {
            var userLikedPosts = await _context.Set<Dictionary<string, object>>("posts_liked")
                .Where(p => (int)p["likedPostId"] == postId)
                .ToListAsync();
            if (userLikedPosts.Any())
            {
                _context.Remove(userLikedPosts);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteFromUsersSavedPost(int postId)
        {
            var userSavedPosts = await _context.Set<Dictionary<string, object>>("users_saved_post")
                .Where(p => (int) p["savedPostId"] == postId)
                .ToListAsync();
            if (userSavedPosts.Any())
            {
                _context.Remove(userSavedPosts);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> DeletePost(int postId, int userId)
        {
            var post = await FindPostById(postId);
            var user = await _userRepository.FindUserById(userId);

            if (post.user.Id != user.Id)
            {
                throw new Exception("Can't delete another user's post!");
            }
            await DeleteFromUsersSavedPost(post.Id);
            await DeleteFromUsersLikedPost(post.Id);

            _context.posts.Remove(post);
            await _context.SaveChangesAsync();

            return "Successful delete post";
        }

        public async Task<Post> FindPostById(int postId)
        {
            var post = await _context.posts
                .Where(p => p.Id == postId)
                .Include(p => p.saved)
                .Include(p => p.liked)
                .Include(p=>p.comments)
                .FirstOrDefaultAsync();
            if (post == null)
            {
                throw new Exception("Post not found!");
            }
            return post;
        }

        public async Task<ActionResult<IEnumerable<Post>>> FindPostsByUserId(int userId)
        {
            var posts = await _context.posts
                .Where(p => p.user.Id == userId)
                .Include(p => p.user)
                .ToListAsync();
            return posts;
        }

        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            return await _context.posts
                .Include(p => p.user) 
                .Include(p => p.comments)
                 .ThenInclude(c => c.user)
                .Include(p => p.liked)
                .ToListAsync();
        }

        public async Task<Post> SavePost(int postId, int userId)
        {
            var post = await FindPostById(postId);
            var user = await _userRepository.FindUserById(userId);

            if (user.savedPost.Contains(post))
            {
                user.savedPost.Remove(post);
                post.saved.Remove(user);
            }
            else
            {
                user.savedPost.Add(post);
                post.saved.Add(user);
            }
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> LikePost(int postId, int userId)
        {
            var post = await FindPostById(postId);
            var user = await _userRepository.FindUserById(userId);

            if (user.likedPost.Contains(post))
            {
                user.likedPost.Remove(post);
                post.liked.Remove(user);
            }
            else
            {
                user.likedPost.Add(post);
                post.liked.Add(user);
            }
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
