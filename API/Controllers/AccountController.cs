using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController: BaseAPIController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService) 
        { 
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO) 
        {
            if (await userExist(registerDTO.username)) return BadRequest("User Already Exist");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDTO.username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                username = user.UserName,
                token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var appUser = await _context.Users.SingleOrDefaultAsync(user => user.UserName == loginDTO.username.ToLower());
            if (appUser == null) return Unauthorized("Username dose not match");

            using var hmac = new HMACSHA512(appUser.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));

            for (int i = 0; i < computedHash.Length; i++) {
                if (computedHash[i] != appUser.PasswordHash[i]) return Unauthorized("Password dose not match");
            }

            return new UserDTO
            {
                username = appUser.UserName,
                token = _tokenService.CreateToken(appUser)
            };
        }

        private async Task<bool> userExist(string username)
        {
            return await _context.Users.AnyAsync(user => 
                user.UserName == username.ToLower());
        }
    }
}
