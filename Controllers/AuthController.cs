using AutoMapper;
using fark_t_backend.Dto;
using fark_t_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fark_t_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Users user = new Users();
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _config = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Users>> Register(CreateUserDto request)
        {
            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var users = _mapper.Map<Users>(request);
            users.Id = Guid.NewGuid();

            user = users;
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Users>> Login(LoginDto request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
