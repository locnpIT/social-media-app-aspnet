using Microsoft.AspNetCore.Mvc;
using social_media_app.Models;

namespace social_media_app.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> FindUserByEmail(string email);
        public string EncodePassword(User user, string plainPassword);
        public bool VerifyPassword(User user, string plainPassword, string hashedPassword);
        public Task<User> FindUserByJwt(string jwt);
        public Task<List<User>> SearchUser(string query);
        public Task<ActionResult<IEnumerable<User>>> GetAllUsers();
        public Task<User> FollowUser(int reqUserId, int userId2);
        public Task<User> FindUserById(int userId);
        public Task<User> DeleteUser(int userId);
        public Task<User> UpdateUser(User user, int userId);
    }
}
