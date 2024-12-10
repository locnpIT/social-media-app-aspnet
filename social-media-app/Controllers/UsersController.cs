using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.Models;

namespace social_media_app.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SocialMediaContext _context;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public UsersController(SocialMediaContext context, ITokenService tokenService, IUserRepository userRepository)
        {
            _context = context;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpGet("users/profile")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserFromToken([FromHeader(Name = "Authorization")] string jwtToken)
        {

            try
            {
                jwtToken = jwtToken.Replace("Bearer ", string.Empty);
                var user = await _userRepository.FindUserByJwt(jwtToken);
                user.password = null;
                return Ok(user);
            }
            catch (SecurityTokenException ex) 
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        [HttpGet("users/search")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> SearchUser([FromQuery(Name = "query")] string query)
        {
            return await _userRepository.SearchUser(query);
        }

        [HttpPut("users/follow/{userId2}")]
        [Authorize]
        public async Task<ActionResult<User>> FollowUserHandler([FromHeader(Name = "Authorization")] string jwtToken, int userId2)
        {
            try
            {
                jwtToken = jwtToken.Replace("Bearer ", string.Empty);
                var currentUser = await _userRepository.FindUserByJwt(jwtToken);
                var user = await _userRepository.FollowUser(currentUser.Id, userId2);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpDelete("users/{userId}")]
        [Authorize]
        public async Task<string> DeleteUserById(int userId)
        {
            var user = await _userRepository.DeleteUser(userId);
            return "Remove user have id = " + userId + " success!";
        }

        [HttpPut("users")]
        [Authorize]
        public async Task<ActionResult<User>> UpdateUserById([FromHeader(Name = "Authorization")] string jwtToken, [FromBody] User user)
        {
            try
            {
                jwtToken = jwtToken.Replace("Bearer ", string.Empty);
                var reqUser = await _userRepository.FindUserByJwt(jwtToken);
                var updateUser = await _userRepository.UpdateUser(user, reqUser.Id);

                return Ok(updateUser);
            }
            catch (Exception)
            {
                throw new Exception("Update invalid!");
            }
        }

        [HttpGet("users/{userId}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            return await _userRepository.FindUserById(userId);
        }
    }
}
