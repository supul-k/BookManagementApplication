using BookManagementApplication.DbAccess;
using BookManagementApplication.DTO;
using BookManagementApplication.Interfaces;
using BookManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagementApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(ApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("create-user", Name ="CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO request)
        {
            try
            {
                UserModel user = new UserModel();
                user.Id = Guid.NewGuid().ToString();
                user.UserName = request.UserName;
                user.Email = request.Email;
                user.Role = request.Role;
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return Created(string.Empty, "User Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login-user", Name = "LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO request)
        {
            try
            {
                var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (result == null)
                {
                    return BadRequest("User Not Found");
                }

                UserModel user = result as UserModel;

                bool isVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
                if (!isVerified)
                {
                    return BadRequest("Password Incorrect");
                }

                var jwtToken = await _jwtService.GenerateJWT(user);
                if (!jwtToken.Status)
                {
                    return BadRequest("Token Error");
                }

                return Ok(jwtToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
