
using social_media_app.Models;

namespace social_media_app.Interfaces
{
    public interface ICommentRepository
    {
        public Task<Comment> CreateComment(Comment comment, int postId, int userId);
        public Task<Comment> LikeComment(int commentId, int userId);
        public Task<Comment> FindCommentById(int id);
    }
}
 