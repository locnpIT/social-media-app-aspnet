using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using social_media_app.Data;
using social_media_app.DTOs.Users;
using social_media_app.Interfaces;
using social_media_app.Models;
using social_media_app.Response;

namespace social_media_app.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SocialMediaContext _context;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthController(SocialMediaContext context, ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _context = context;
            _userRepository = userRepository;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthResponse>> CreateUser(SignupDTO user)
        {
            var isExist = await _userRepository.FindUserByEmail(user.email);
            if (isExist != null)
            {
                throw new Exception("This email already used with another account!");
            }

            var newUser = new User
            {
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                password = _userRepository.EncodePassword(new User(), user.password)
            };
            _context.users.Add(newUser);
            await _context.SaveChangesAsync();

            return new AuthResponse(_tokenService.CreateToken(newUser), "Register success!");
        }

        [HttpPost("signin")]
        public async Task<ActionResult<AuthResponse>> SignIn([FromBody] SigninDTO signInRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.FindUserByEmail(signInRequest.email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username");
            }

            bool isPasswordValid = _userRepository.VerifyPassword(user, user.password, signInRequest.password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Password is incorrect");
            }

            return new AuthResponse(_tokenService.CreateToken(user), "Login success!");
        }
    }
}
