using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace social_media_app.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialMediaContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        public UserRepository(SocialMediaContext context, PasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _passwordHasher = passwordHasher;
            _context = context;
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        }

        public async Task<User> DeleteUser(int userId)
        {
            var user = await _context.users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public string EncodePassword(User user, string plainPassword)
        {
            return _passwordHasher.HashPassword(user, plainPassword);
        }

        public async Task<User?> FindUserByEmail(string email)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.email == email);
            return user;
        }

        public async Task<User> FindUserById(int userId)
        {
            var user = await _context.users
                .Where(u => u.Id == userId)
                .Include(u => u.savedPost)
                .Include(u => u.likedPost)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Not found!");
            }
            return user;
        }

        public async Task<User> FindUserByJwt(string jwt)
        {
            try
            {
                jwt = jwt.Replace("Bearer ", string.Empty);
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = _key
                };

                var principal = tokenHandler.ValidateToken(jwt, validationParameters, out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "UserId");
                if (userIdClaim == null)
                {
                    throw new Exception("Invalid token: UserId claim is missing");
                }

                if (!int.TryParse(userIdClaim.Value, out int userId))
                {
                    throw new Exception("Invalid UserId format in token");
                }

                var user = await _context.users.FindAsync(userId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Token validation failed", ex);
            }
        }

        public async Task<User> FollowUser(int reqUserId, int userId2)
        {
            User reqUser = await FindUserById(reqUserId);
            User user2 = await FindUserById(userId2);

            if (reqUser.followers.Contains(userId2))
            {
                reqUser.followers.Remove(user2.Id);
                user2.following.Remove(reqUser.Id);
            }
            else
            {
                reqUser.followers.Add(user2.Id);
                user2.following.Add(reqUser.Id);
            }

            await _context.SaveChangesAsync();

            return reqUser;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _context.users
                .Include(u => u.savedPost)
                .ToListAsync();
        }

        public async Task<List<User>> SearchUser(string query)
        {
            var users = await _context.users
                .Where(u => u.firstName.Contains(query.Trim()) || u.lastName.Contains(query.Trim()) || u.email.Contains(query.Trim()))
                .ToListAsync();
            return users;
        }

        public async Task<User> UpdateUser(User user, int userId)
        {
            var userUpdate = await _context.users.FindAsync(userId);
            if (userUpdate == null)
            {
                throw new Exception("User not found");
            }
            userUpdate.firstName = user.firstName ?? userUpdate.firstName;
            userUpdate.lastName = user.lastName ?? userUpdate.lastName;
            userUpdate.email = user.email ?? userUpdate.email;
            userUpdate.gender = user.gender ?? userUpdate.gender;

            await _context.SaveChangesAsync();

            return userUpdate;
        }

        public bool VerifyPassword(User user, string hashedPassword, string plainPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }

    }
}