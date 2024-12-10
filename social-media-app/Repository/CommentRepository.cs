using Microsoft.EntityFrameworkCore;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SocialMediaContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        public CommentRepository(SocialMediaContext context, IUserRepository userRepository, IPostRepository postRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }
        public async Task<Comment> CreateComment(Comment comment, int postId, int userId)
        {
            var user = await _userRepository.FindUserById(userId);
            var post = await _postRepository.FindPostById(postId);

            var newComment = new Comment
            {
                user = user,
                content = comment.content,
                createAt = DateTime.Now,
            };

            post.comments.Add(newComment);

            _context.comments.Add(newComment);
            await _context.SaveChangesAsync();

            return newComment;

        }

        public async Task<Comment> LikeComment(int commentId, int userId)
        {
            var comment = await FindCommentById(commentId);
            var user = await _userRepository.FindUserById(userId);

            if (!comment.liked.Contains(user))
            {
                comment.liked.Add(user);
            }
            else
            {
                comment.liked.Remove(user);
            }
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> FindCommentById(int id)
        {
            var comment = await _context.comments
                .Include(c => c.user)
                .Include(c => c.liked)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                throw new Exception("Cannot find comment with id = " + id);
            }
            return comment;
        }
    }
}
