using Microsoft.AspNetCore.Mvc;
using social_media_app.Models;

namespace social_media_app.Interfaces
{
    public interface IPostRepository
    {
        public Task<Post> CreatePost(Post post, int userId);
        public Task<string> DeletePost(int postId, int userId);
        public Task<Post> FindPostById(int postId);
        public Task DeleteFromUsersSavedPost(int postId);
        public Task DeleteFromUsersLikedPost(int postId);
        public Task<ActionResult<IEnumerable<Post>>> GetAllPosts();
        public Task<ActionResult<IEnumerable<Post>>> FindPostsByUserId(int userId);
        public Task<Post> SavePost(int postId, int userId);
        public Task<Post> LikePost(int postId, int userId);
    }
}
